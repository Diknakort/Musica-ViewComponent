using BaseDatosMusica.Models;
using BaseDatosMusica.ViewModels;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Drawing;
using BaseDatosMusica.Services.Repositorio;

namespace BaseDatosMusica.Services
{
    public class DiscosesSinCancionesBuilder (IGenericRepositorio<Disco> _discos, IGenericRepositorio<Cancione> _canciones): IDiscosSinCancionesBuilder
    {

        public List<DiscoSinCancion> dameDiscoSinCanciones(string nombre)
        {
            var resultado =
                from DiscosA in  _discos.DameTodos().Result
                join CancionesB in _canciones.DameTodos().Result
                    on DiscosA.Id equals CancionesB.DiscosId into gj
                from subCancionesB in gj.DefaultIfEmpty()
                where subCancionesB == null && DiscosA.Nombre == nombre
                //orderby subCancionesB.Titulo
                select new DiscoSinCancion()
                {
                    NombreDisco = DiscosA.Nombre,
                    Id = DiscosA.Id,
                    NombreCancion= subCancionesB.Titulo
                };
            return resultado.ToList();


            //var conJoin = from DiscosA in _context.Discos
            //    join CancionesB in _context.Canciones
            //        on DiscosA.Id equals CancionesB.Id
            //    where DiscosA.Canciones != CancionesB
            //    select new
            //    {
            //        Id = DiscosA.Id,
            //        NombreDisco = DiscosA.Nombre,
            //        NombreCancion = CancionesB.Titulo,
            //    };
            //var agrupado = from linea in conJoin.ToList()
            //    group linea by linea.Id
            //    into g
            //    select g;
            //// Se crea una colección objetos que contienen el atributo superior en la consulta
            //List<DiscosSinCancionesViewModel> listado = new();
            //foreach (var item in agrupado)
            //{
            //    //ColorViewModel viewModel = new ColorViewModel() { ColorProducto = item.Key };
            //    DiscosSinCancionesViewModel viewModel = new() { Id = item.Key };
            //    viewModel.DiscoHuerfano = new List<DiscoSinCancion>();

            //    foreach (var ventas in item)
            //    {
            //        DiscoSinCancion Venta = new DiscoSinCancion()
            //        {
            //            Id = ventas.Id,
            //            NombreCancion = ventas.NombreCancion,

            //        };
            //        viewModel.DiscoHuerfano.Add(Venta);
            //    }
            //    listado.Add(viewModel);
            //}
            //return listado;
        }
    }


}
