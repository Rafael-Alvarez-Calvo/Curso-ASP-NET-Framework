using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<ClientesCLS> listaClientes = null;

            using (var bd = new BDPasajeEntities())
            {
                listaClientes = (from clientes in bd.Cliente
                                 where clientes.BHABILITADO == 1
                                 select new ClientesCLS
                                 {
                                     iidcliente = clientes.IIDCLIENTE,
                                     nombre = clientes.NOMBRE,
                                     appaterno = clientes.APPATERNO,
                                     apmaterno = clientes.APMATERNO,
                                     email = clientes.EMAIL,
                                     direccion = clientes.DIRECCION,
                                     iidsexo = (int)clientes.IIDSEXO,
                                     telefonofijo = clientes.TELEFONOFIJO,
                                     telefonocelular = clientes.TELEFONOCELULAR,

                                 }).ToList();
            }
                return View(listaClientes);
        }

        List<SelectListItem> listaSexo;
        private void setSexSelector()
        {
            using( var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                                where sexo.BHABILITADO == 1
                                select new SelectListItem
                                {
                                    Text = sexo.NOMBRE,
                                    Value = sexo.IIDSEXO.ToString()

                                }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.lista = listaSexo;
            }
        }

        public ActionResult Agregar()
        {
            setSexSelector();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClientesCLS oClientesCLS)
        {

            if(!ModelState.IsValid)
            {
                setSexSelector();  //Necesitamos volver a llenar el selector cuando se valide algun campo y salga erroneo

                return View(oClientesCLS);
            }
            else
            {
                using(var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = new Cliente();

                    oCliente.NOMBRE = oClientesCLS.nombre;
                    oCliente.APPATERNO = oClientesCLS.appaterno;
                    oCliente.APMATERNO = oClientesCLS.apmaterno;
                    oCliente.EMAIL = oClientesCLS.email;
                    oCliente.DIRECCION = oClientesCLS.direccion;
                    oCliente.IIDSEXO = oClientesCLS.iidsexo;
                    oCliente.TELEFONOFIJO = oClientesCLS.telefonofijo;
                    oCliente.TELEFONOCELULAR = oClientesCLS.telefonocelular;
                    oCliente.BHABILITADO = 1;

                    bd.Cliente.Add(oCliente);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ClientesCLS oClienteCLS = new ClientesCLS();

            using( var bd = new BDPasajeEntities())
            {
                
                setSexSelector();

                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();

                //oClienteCLS = Es el objeto de la vista
                //oCliente = es el objeto de la bd
                //Por eso en este caso el valor de cada parametro se introduce en lka vista para poder pintarlo

                oClienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.appaterno = oCliente.APPATERNO;
                oClienteCLS.apmaterno = oCliente.APMATERNO;
                oClienteCLS.telefonofijo = oCliente.TELEFONOFIJO;
                oClienteCLS.telefonocelular = oCliente.TELEFONOCELULAR;
                oClienteCLS.email = oCliente.EMAIL;
                oClienteCLS.iidsexo = (int)oCliente.IIDSEXO;

            }

            return View(oClienteCLS);
        }


        [HttpPost]
        public ActionResult Editar(ClientesCLS oClienteCLS)
        {
            if(!ModelState.IsValid)
            {
                setSexSelector();
                return View(oClienteCLS);
            }
            else
            {
                int idCliente = oClienteCLS.iidcliente;

                using( var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();

                    //oClienteCLS = Es el objeto de la vista
                    //oCliente = es el objeto de la bd
                    //Por eso en este caso el valor de cada parametro se introduce en la bd para insertarlo

                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.IIDSEXO = oClienteCLS.iidsexo;

                    bd.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Borrar(int id)
        {
            using( var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(res => res.IIDCLIENTE.Equals(id)).First();
                oCliente.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}