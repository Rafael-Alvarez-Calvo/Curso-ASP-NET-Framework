using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class MarcaController : Controller
    {

        //El controlador gestiona todas las conexiones con la base de datos
        // GET: Marca
        public ActionResult Index()
        {

            List<MarcaCLS> listaMarca = null; //Crea una variable de listaMarca con el valor vacio, abajo rellena la lista con la query
                                              //Se crea aqui la variable porque si se crea dentro del using, View no reconoceria la variable

            using (var bd = new BDPasajeEntities()) //El using abre la conexión con la base de datos
            {
                listaMarca = (from marca in bd.Marca  // => select * from Marca where marca.BHabilitado == 1
                               where marca.BHABILITADO == 1
                                select new MarcaCLS
                                {
                                    iidmarca = marca.IIDMARCA,
                                    nombre = marca.NOMBRE,
                                    descripcion = marca.DESCRIPCION

                                }).ToList();

            }


            return View(listaMarca);
        }

        public ActionResult Agregar()
        {
            return View();
        }
        
        /*[HttpPost]
        public ActionResult Agregar()
        {
            return View();
        }*/
    }
}