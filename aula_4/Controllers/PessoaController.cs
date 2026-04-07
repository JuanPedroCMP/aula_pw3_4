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

        private void IniciarLista()
        {
            var listaReserva = new List<Pessoa> {
                    new() { Nome = "Helder", Idade = 115 },
                    new() { Nome = "Alex", Idade = 35 },
                    new() { Nome = "Benir", Idade = 18 },
                };

            var lista = Ler();

            if (lista.Count == 0) { 
                Gravar(listaReserva);
            }
        }

        private void Gravar(List<Pessoa> ListPessoa)
        {
            string pessoas = JsonConvert.SerializeObject(ListPessoa);
            HttpContext.Session.SetString("pessoas", pessoas);

            return;
        }

        private List<Pessoa> Ler()
        {
            var lista = new List<Pessoa>();

            Gravar(lista);

            var pessoaReserva = Pessoa.Lista;


            string pessoas = HttpContext.Session.GetString("pessoas");

            if (pessoas == "[]")
            {
                pessoas = JsonConvert.SerializeObject(pessoaReserva);
            }

            try
            {
                lista = JsonConvert.DeserializeObject<List<Pessoa>>(pessoas); // erro aqui

                if (lista.Count == 0) {
                    IniciarLista();
                  Ler();
                }
            }
            catch {
                IniciarLista();
               Ler();
            }
           


            return lista;
        }

        public ActionResult Index()
        { 
            return View(Ler());
        }


        // GET: PessoaController/Details/5
        public ActionResult Details(int id)
        {
            return View(Pessoa.Lista[id]);
        }

        // GET: PessoaController/Create
        public ActionResult Create()
        {
            return View(new Pessoa());
        }

        // POST: PessoaController/Create
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

        // GET: PessoaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,  Pessoa pessoa)
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

        // GET: PessoaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaController/Delete/5
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
