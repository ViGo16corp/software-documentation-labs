using Microsoft.AspNetCore.Mvc;
using lab3.Models;

namespace lab3.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();

        // Показати список
        public IActionResult Index()
        {
            return View(users);
        }

        // Відкрити форму
        public IActionResult Create()
        {
            return View();
        }

        // Додати користувача
        [HttpPost]
        public IActionResult Create(User user)
        {
            user.Id = users.Count + 1;
            users.Add(user);
            return RedirectToAction("Index");
        }

        // Видалити
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
            return RedirectToAction("Index");
        }

        // Редагування (відкрити)
        public IActionResult Edit(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        // Редагування (зберегти)
        [HttpPost]
        public IActionResult Edit(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
            }
            return RedirectToAction("Index");
        }
    }
}