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
        public ActionResult Index(MarcaCLS oMarcaCLS)
        {

            string nombreMarca = oMarcaCLS.nombre;

            List<MarcaCLS> listaMarca = null; //Crea una variable de listaMarca con el valor vacio, abajo rellena la lista con la query
                                              //Se crea aqui la variable porque si se crea dentro del using, View no reconoceria la variable

            using (var bd = new BDPasajeEntities()) //El using abre la conexión con la base de datos
            {
                if (nombreMarca == null)
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
                else
                {
                    listaMarca = (from marca in bd.Marca  // => select * from Marca where marca.BHabilitado == 1
                                  where marca.BHABILITADO == 1
                                  && marca.NOMBRE.Contains(nombreMarca)
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION

                                  }).ToList();
                }
            }

            return View(listaMarca);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            int registrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            using( var bd = new BDPasajeEntities())
            {
                //registrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
                registrosEncontrados = bd.Marca.Count(p => p.NOMBRE.Equals(nombreMarca));
            }

            if(!ModelState.IsValid || registrosEncontrados >= 1 )
            {
                if (registrosEncontrados >= 1) oMarcaCLS.mensajeError = "El nombre de la marca ya existe";
                    return View(oMarcaCLS);
            }
            else
            {
                using(var bd= new BDPasajeEntities())
                {
                    Marca oMarca = new Marca(); // Este marca() es entity framework
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    oMarca.BHABILITADO = 1;
                    bd.Marca.Add(oMarca);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            MarcaCLS oMarcaCLS = new MarcaCLS();

            using( var bd = new BDPasajeEntities())
            {
                //Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                Marca oMarca = bd.Marca.FirstOrDefault(p => p.IIDMARCA.Equals(id));
                //Donde el id de marca que recibo sea igual al id de mi tabla en base de datos, lo meto en una clase, esto devuelve una lista por eso se le pone First() para que devuelva un objeto

                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;
            }

            return View(oMarcaCLS);
        }

        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {

            int registrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            int idMarca = oMarcaCLS.iidmarca;

            using (var bd = new BDPasajeEntities())
            {
                registrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(idMarca)).Count();
            }

            if (!ModelState.IsValid || registrosEncontrados >=1)
            {
                if(registrosEncontrados >= 1) oMarcaCLS.mensajeError = "El nombre de la marca ya existe";
                return View(oMarcaCLS);
            }
            else
            {

                using (var bd = new BDPasajeEntities())
                {
                    Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();

                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;

                    bd.SaveChanges();
                }
            }
            
            return RedirectToAction("Index");
        }
        
        
        public ActionResult Borrar(int id)
        {
           using( var bd = new BDPasajeEntities())
           {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();

                oMarca.BHABILITADO = 0;
                bd.SaveChanges();
           }

            return RedirectToAction("Index");
        }
    }
}