﻿using ITGoShop_F_Ver2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ITGoShop_F_Ver2.Controllers
{
    public class BlogManagementController : Controller
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public BlogManagementController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult add_content()
        {
            return View();
        }
        [Obsolete]
        public IActionResult SaveContent(Blog newBlog, List<IFormFile> Image)
        {
            // Lưu ảnh sản phẩm vào trước
            string path = Path.Combine(this.Environment.WebRootPath, "public/images_upload/blog");
            foreach (IFormFile postedFile in Image)
            {
                // Lấy tên file
                newProduct.ProductImage = DateTime.Now.ToString("yyyy_MM_dd_HHmmss_") + postedFile.FileName;
                // Lưu file vào project
                string fileName = Path.GetFileName(DateTime.Now.ToString("yyyy_MM_dd_HHmmss_") + postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
            }

            var context = new ITGoShopLINQContext();
            context.saveProduct(newProduct);
            return RedirectToAction("add_product");

        }
        public IActionResult view_content()
        {
            ITGoShopContext context = HttpContext.RequestServices.GetService(typeof(ITGoShop_F_Ver2.Models.ITGoShopContext)) as ITGoShopContext;
            ViewBag.AllBlog = context.getAllBlog();
            return View();
        }
        public void unactive_blog(int ProductId)
        {
            ITGoShopContext context = HttpContext.RequestServices.GetService(typeof(ITGoShop_F_Ver2.Models.ITGoShopContext)) as ITGoShopContext;
            context.updateProductStatus(ProductId, 0);
        }
        public void active_blog(int ProductId)
        {
            ITGoShopContext context = HttpContext.RequestServices.GetService(typeof(ITGoShop_F_Ver2.Models.ITGoShopContext)) as ITGoShopContext;
            context.updateProductStatus(ProductId, 1);
        }

        public void delete_blog(int ProductId)
        {
            ITGoShopContext context = HttpContext.RequestServices.GetService(typeof(ITGoShop_F_Ver2.Models.ITGoShopContext)) as ITGoShopContext;
            context.deleteProduct(ProductId);
        }
    }
}
