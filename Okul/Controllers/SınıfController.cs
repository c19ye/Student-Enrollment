using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Okul.Data;
using Okul.Models;

namespace Okul.Controllers
{
    public class SınıfController : Controller
    {
        private readonly VeritabaniContext _context;

        public SınıfController(VeritabaniContext context)
        {
            _context = context;
        }

        // GET: Sınıf
        public async Task<IActionResult> Index()
        {
            return View(await _context.siniflar.ToListAsync());
        }

        // GET: Sınıf/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sınıf = await _context.siniflar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sınıf == null)
            {
                return NotFound();
            }

            return View(sınıf);
        }

        // GET: Sınıf/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sınıf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] Sınıf sınıf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sınıf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sınıf);
        }

        // GET: Sınıf/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sınıf = await _context.siniflar.FindAsync(id);
            if (sınıf == null)
            {
                return NotFound();
            }
            return View(sınıf);
        }

        // POST: Sınıf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] Sınıf sınıf)
        {
            if (id != sınıf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sınıf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SınıfExists(sınıf.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sınıf);
        }

        // GET: Sınıf/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sınıf = await _context.siniflar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sınıf == null)
            {
                return NotFound();
            }

            return View(sınıf);
        }

        // POST: Sınıf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sınıf = await _context.siniflar.FindAsync(id);
            //önce silmeyi ve veritabanına kaydetmeyi deneyecek 
            try
            {
               
                _context.siniflar.Remove(sınıf);
                await _context.SaveChangesAsync();
            }
            //olmazsa şayet şu işlerimleri yapsın diyoruz
            catch (Exception e)
            {
                //eğer hata verirse sınıfınogrencielri actionuna yönlendirecek 
                //uyari=1 yazarsak 1 yazarak yönlendirme yapacağız
                return RedirectToAction("sınıfınogrencileri", "Ogrencis", new {id = sınıf.Id,Uyari=1});
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool SınıfExists(int id)
        {
            return _context.siniflar.Any(e => e.Id == id);
        }
    }
}
