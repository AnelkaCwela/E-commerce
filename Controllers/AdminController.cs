using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using OurShop.Models.Databinding;
using OurShop.Models;
using OurShop.Models.DataModel;

namespace Thumeka.Controllers
{
   [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManger;
        private readonly UserManager<UserPlusModel> _UserManger;
        private readonly SignInManager<UserPlusModel> _signInManager;
        public AdminController(RoleManager<IdentityRole> RoleManger, UserManager<UserPlusModel> UserManger, SignInManager<UserPlusModel> signInManager)
        {
            this._RoleManger = RoleManger;
            this._UserManger = UserManger;
            this._signInManager = signInManager;
           
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
       
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _RoleManger.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Admin");
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            return View(model);
        }
       
        [HttpGet]
        public IActionResult ListRole()
        {
            var role = _RoleManger.Roles;

            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await _RoleManger.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {Id} cannet be found";
                return View("NotFound");
            }
            var model = new EditRoleModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            //if(ConnectionState.Open==State)
            //{ 

            //}
            foreach (var user in _UserManger.Users)
            {
                //
                if (await _UserManger.IsInRoleAsync(user, role.Name))
                {
                    //mode.User.Add(use.Name);
                    model.Users.Add(user.UserName);


                }
            }
            return View(model);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await _RoleManger.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _RoleManger.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string RoleId)
        {
            ViewBag.RoleId = RoleId;
            var role = await _RoleManger.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {RoleId} cannet be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            //List<StaffModel> sample=_usersample != null.
            foreach (var user in _UserManger.Users)
            {
                //StaffModel sample = _user.GetStaffByEmail(user.UserName);
                //if ()
                {
                    var userRoleViewModel = new UserRoleViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName

                    };
                    if (await _UserManger.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }
                    model.Add(userRoleViewModel);
                }
            }
            return View(model);
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> AddUserToRole(List<UserRoleViewModel> model, string RoleId)
        {
            //RoleId = ViewBag.RoleId;

            var role = await _RoleManger.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.errMessage = $"Role with Id = {model[0].RoleIdd} cannet be found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _UserManger.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _UserManger.IsInRoleAsync(user, role.Name)))
                {
                    result = await _UserManger.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _UserManger.IsInRoleAsync(user, role.Name))
                {
                    result = await _UserManger.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = RoleId });
                }
            }
            return RedirectToAction("EditRole", new { id = RoleId });
        }
  
    }
}
