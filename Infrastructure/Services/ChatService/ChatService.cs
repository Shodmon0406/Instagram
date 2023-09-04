﻿using System.Net;
using Domain.Dtos.ChatDto;
using Domain.Dtos.MessageDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ChatService;

public class ChatService : IChatService
{
    private readonly DataContext _context;

    public ChatService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<List<GetChatDto>>> GetChats(string? userId)
    {
        try
        {
            // if (string.IsNullOrEmpty(userId)) return new Response<List<GetChatDto>>(HttpStatusCode.BadRequest, "");
            var chats = await _context.Chats.Where(u => u.SendUserId == userId || u.ReceiveUserId == userId)
                .Select(c => new GetChatDto()
                {
                    ChatId = c.ChatId,
                    SendUserId = c.SendUserId,
                    ReceiveUserId = c.ReceiveUserId
                }).ToListAsync();
            return new Response<List<GetChatDto>>(chats);
        }
        catch (Exception e)
        {
            return new Response<List<GetChatDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetMessageDto>>> GetChatById(ChatDto chat, string userId)
    {
        try
        {
            var result = await _context.Chats.FirstOrDefaultAsync(u =>
                (u.SendUserId == userId && u.ReceiveUserId == chat.ReceiveUserId) ||
                (u.ReceiveUserId == userId) && u.SendUserId == chat.ReceiveUserId);
            if (result == null)
            {
                var newChat = new Chat()
                {
                    SendUserId = userId,
                    ReceiveUserId = chat.ReceiveUserId
                };
                await _context.Chats.AddAsync(newChat);
                await _context.SaveChangesAsync();
                chat.ChatId = newChat.ChatId;
            }
            else
            {
                chat.ChatId = result.ChatId;
            }
          
            var response = await (from c in _context.Chats
                join m in _context.Messages on c.ChatId equals m.ChatId
                where c.ChatId == chat.ChatId
                select new GetMessageDto()
                {
                    MessageId = m.MessageId,
                    ChatId = m.ChatId,
                    UserId = m.UserId,
                    MessageText = m.MessageText,
                    SendMassageDate = m.SendMassageDate
                }).OrderByDescending(x => x.SendMassageDate).ToListAsync();
            return new Response<List<GetMessageDto>>(response);
        }
        catch (Exception e)
        {
            return new Response<List<GetMessageDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> SendMessage(MessageDto message, string userId)
    {
        try
        {
            var chat = await _context.Chats.FindAsync(message.ChatId);
            if (chat == null) return new Response<int>(HttpStatusCode.BadRequest, "Chat not found!");
            var newMessage = new Message()
            {
                ChatId = message.ChatId,
                UserId = userId,
                MessageText = message.MessageText,
                SendMassageDate = DateTime.UtcNow
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync();
            return new Response<int>(newMessage.MessageId);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMessage(int massageId)
    {
        try
        {
            var message = await _context.Messages.FindAsync(massageId);
            if (message == null) return new Response<bool>(false);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteChat(int chatId)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat == null) return new Response<bool>(false);
        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();
        return new Response<bool>(true);
    }
}