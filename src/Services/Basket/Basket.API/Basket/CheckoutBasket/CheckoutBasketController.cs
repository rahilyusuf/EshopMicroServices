using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.CheckoutBasket
{
    public class CheckoutBasketController : Controller
    {
        // GET: CheckoutBasketController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckoutBasketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CheckoutBasketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckoutBasketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutBasketController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckoutBasketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckoutBasketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckoutBasketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
