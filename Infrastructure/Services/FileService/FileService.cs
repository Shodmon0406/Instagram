﻿using System.Net;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.FileService;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly DataContext _context;

    public FileService(IWebHostEnvironment hostEnvironment,DataContext context)
    {
        _hostEnvironment = hostEnvironment;
        _context = context;
    }
    
    public Response<string> CreateFile(IFormFile file)
    {
        try
        {
            var find = _context.Categories.Find(1,1);
            var filename = $"{DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, "images", filename);
            using var stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            return new Response<string>(filename);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public Response<bool> DeleteFile(string file)
    {
        try
        {
            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, "images", file);
            File.Delete(fullPath);
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}