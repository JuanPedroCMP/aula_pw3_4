using aula_4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aula_4.Controllers
{
    public class PessoaController : Controller
    {
        // GET: PessoaController
        //public ActionResult Index()
        //{
        //    var lista = Pessoa.Lista;

        //    return View(lista);
        //}

        private List<Pessoa> Ler()
        {
            List<Pessoa> lista;
            string pessoas = HttpContext.Session.GetString("pessoas");

            if (String.IsNullOrEmpty(pessoas))
                lista = IniciarLista();
            else
            {
                lista = JsonConvert.DeserializeObject<List<Pessoa>>(pessoas);
                if (lista.Count == 0)
                    IniciarLista();
            }

            return lista;
        }

        private List<Pessoa> IniciarLista()
        {
            List<Pessoa> lista = Pessoa.Lista;
            Gravar(lista);
            return lista;
        }

        private void Gravar(List<Pessoa> lista)
        {
            string pessoas = JsonConvert.SerializeObject(lista);
            HttpContext.Session.SetString("pessoas", pessoas);
        }

        /// ///////////////////////////////////////


        public ActionResult Index()
        { 
            return View(Ler());
        }


        // GET: PessoaController/Details/5
        public ActionResult Details(int id)
        {
            var lista = Ler();
            return View(lista[id]);
        }

        // GET: PessoaController/Create
        public ActionResult Create()
        {
            return View(new Pessoa());
        }

        // POST: PessoaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                var lista = Ler();
                lista.Add(pessoa);
                Gravar(lista); //TODO Fazer LerLista() e AtualizarLista()
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Ler()[id]);
        }

        // POST: PessoaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,  Pessoa pessoa)
        {
            try
            {
                var lista = Ler();
                lista[id] = pessoa;
                Gravar(lista);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            var lista = Ler();
            return View(lista[id]);
        }

        // POST: PessoaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pessoa pessoa)
        {
            try
            {
                var lista = Ler();  
                lista.RemoveAt(id);
                Gravar(lista);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
