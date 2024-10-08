using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMvcFuncional.Data;
using AppMvcFuncional.Models;

namespace AppMvcFuncional.Controllers
{
    public class AnotacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnotacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Anotacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Anotacao.Where(a => a.Excluido == false).ToListAsync());
        }

        public async Task<IActionResult> Lixeira()
        {
            return View(await _context.Anotacao.Where(a => a.Excluido == true).ToListAsync());
        }

        // GET: Anotacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anotacao = await _context.Anotacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anotacao == null)
            {
                return NotFound();
            }

            return View(anotacao);
        }

        // GET: Anotacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anotacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Prioridade,SubTitulo,Descricao,DataDeCriacao,Editado,DataEdicao,Excluido,DataExclusao")] Anotacao anotacao)
        {
            Anotacao _anotacao = anotacao;
            _anotacao.DataEdicao = DateOnly.FromDateTime(DateTime.Now);
            _anotacao.DataDeCriacao = DateOnly.FromDateTime(DateTime.Now);
            if (ModelState.IsValid)
            {
                _context.Add(_anotacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anotacao);
        }

        // GET: Anotacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anotacao = await _context.Anotacao.FindAsync(id);
            if (anotacao == null)
            {
                return NotFound();
            }
            return View(anotacao);
        }

        // POST: Anotacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Prioridade,SubTitulo,Descricao,DataDeCriacao,Editado,DataEdicao,Excluido,DataExclusao")] Anotacao anotacao)
        {
            if (id != anotacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anotacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnotacaoExists(anotacao.Id))
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
            return View(anotacao);
        }

        // GET: Anotacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anotacao = await _context.Anotacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anotacao == null)
            {
                return NotFound();
            }

            return View(anotacao);
        }

        // POST: Anotacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anotacao = await _context.Anotacao.FindAsync(id);
            if (anotacao != null)
            {
                anotacao.Excluido = true;
                anotacao.DataExclusao = DateOnly.FromDateTime(DateTime.Now);
                _context.Update(anotacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Restaurar")]
        public async Task<IActionResult> Restaurar(int id)
        {
            var anotacao = await _context.Anotacao.FindAsync(id);
            if (anotacao != null)
            {
                anotacao.Excluido = false;
                anotacao.DataExclusao = DateOnly.FromDateTime(DateTime.Now);
                _context.Update(anotacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Lixeira));
        }

        private bool AnotacaoExists(int id)
        {
            return _context.Anotacao.Any(e => e.Id == id);
        }
    }
}
