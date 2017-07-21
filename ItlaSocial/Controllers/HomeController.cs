using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItlaSocial.Models;
using Microsoft.AspNetCore.Identity;
using ItlaSocial.Data;
using Microsoft.AspNetCore.Authorization;
using ItlaSocial.Models.ApplicationViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using AutoMapper;

namespace ItlaSocial.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private IDbRepository _repository;

        public HomeController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IHostingEnvironment env,
            IDbRepository repository)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
            _repository = repository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult PhotoUpload()
        {
            return View();
        }

        [ActionName("PhotoUpload")]
        [HttpPost]
        public async Task<IActionResult> PhotoUploadAsync(ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var userFolder = Path.Combine("images", user.Id);

                var fordel = Path.Combine(_env.WebRootPath, userFolder);
                Directory.CreateDirectory(fordel);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {

                        var fileName = String.Concat(file.GetHashCode().ToString(), new Random().Next().ToString(), ".", file.FileName.Split('.')[file.FileName.Split('.').Length - 1]);

                        using (var fileStream = new FileStream(Path.Combine(fordel, fileName), FileMode.Create))
                        {

                            await file.CopyToAsync(fileStream);

                            var photos = _context.ProfilePhotos.Where(p => p.ApplicationUserId == user.Id && p.Current);
                            if (photos.Count() > 0)
                            {
                                photos.First().Current = false;
                            }

                            _context.ProfilePhotos.Add(new ProfilePhoto
                            {
                                ApplicationUserId = user.Id,
                                MediaUrl = Path.Combine(userFolder, fileName),
                                Current = true
                            });

                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult NewPublication()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ActionName("NewPublication")]
        public async Task<IActionResult> NewPublicationAsync(string title, string description, ICollection<IFormFile> files)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var publication = new Publication()
                {
                    Date = DateTime.UtcNow,
                    ApplicationUserId = user.Id,
                    Title = title,
                    Description = description
                };

                await _context.Publications.AddAsync(publication);
                await _context.SaveChangesAsync();

                if (files.Count() > 0)
                {
                    var userFolder = Path.Combine("images", user.Id, "PublicationMedia");

                    var fordel = Path.Combine(_env.WebRootPath, userFolder);
                    Directory.CreateDirectory(fordel);

                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {

                            var fileName = String.Concat(file.GetHashCode().ToString(),
                                new Random().Next().ToString(), ".",
                                file.FileName.Split('.')[file.FileName.Split('.').Length - 1]);

                            using (var fileStream = new FileStream(Path.Combine(fordel, fileName), FileMode.Create))
                            {

                                await file.CopyToAsync(fileStream);

                                _context.PublicationMedias.Add(new PublicationMedia()
                                {
                                    Publication = publication,
                                    PublicationId = publication.Id,
                                    MediaUrl = Path.Combine(userFolder, fileName)
                                });
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Friends(string friend)
        {
            if (!String.IsNullOrWhiteSpace(friend))
            {
                ViewBag.name = friend;
                var friends = await _repository.FindFriends(friend, User);
                if (friends.Count() > 0)
                {
                    ViewBag.userId = (await _repository.GetUserAsync(User)).Id;
                    return View(friends);
                }
                return View();
            }
            var allfriends = await _repository.FindFriends("", User);
            ViewBag.name = "";
            return View(allfriends);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFriendshipStatus(string friendId, FriendshipStatus status)
        {
            await _repository.ChangeFriendshipStatus(User, friendId, status);
            return RedirectToAction("Friends", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
