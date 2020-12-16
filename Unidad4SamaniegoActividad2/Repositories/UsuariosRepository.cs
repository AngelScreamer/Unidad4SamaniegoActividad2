using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unidad4SamaniegoActividad2.Models;

namespace Unidad4SamaniegoActividad2.Repositories
{
    public class UsuariosRepository<T> where T : class
    {
        public rolessamaniegoContext  Context { get; set; }
        public UsuariosRepository(rolessamaniegoContext context)
        {
            Context = context;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T GetById(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual void Insert(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Add(entidad);
                Context.SaveChanges();
            }
        }
        public virtual void Edit(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Update<T>(entidad);
                Context.SaveChanges();
            }
        }
        public virtual void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }

        public virtual bool Validate(T entidad)
        {
            return true;
        }
    }
    public class AlumnosRepository : UsuariosRepository<Alumno>
    {
        public AlumnosRepository(rolessamaniegoContext context) : base(context) { }

        public Alumno GetAlumnoByNumeroDeControl(string noControl)
        {
            return Context.Alumno.FirstOrDefault(x => x.NumeroDeControl.ToLower() == noControl.ToLower());
        }

        public override bool Validate(Alumno alumno)
        {
            if (string.IsNullOrEmpty(alumno.NumeroDeControl))
            { 
                throw new Exception("Escribe el número de control del alumno."); 
            }
            if (string.IsNullOrEmpty(alumno.Nombre))
            { 
                throw new Exception("Escribe el nombre del alumno."); 
            }
            if (alumno.IdDocente.ToString() == null || alumno.IdDocente <= 0)
            { 
                throw new Exception("Debe asignar un docente al alumno."); 
            }
            if (alumno.NumeroDeControl == "1234")
            { 
                throw new Exception("Este no es un número de control válido."); 
            }
            if (alumno.NumeroDeControl.Length < 8)
            { 
                throw new Exception("Minimo 8 caracteres para el número de control."); 
            }
            if (alumno.NumeroDeControl.Length > 8)
            { 
                throw new Exception("El número de control no debe exceder los 8 dígitos."); 
            }
            return true;
        }
    }
    public class DocenteRepository : UsuariosRepository<Docente>
    {
        public DocenteRepository(rolessamaniegoContext context) : base(context) { }

        public Docente GetDocenteByClave(int clave)
        {
            return Context.Docente.FirstOrDefault(x => x.Clave == clave);
        }
        public Docente GetAlumnoByDocente(int idmaestro)
        {
            return Context.Docente.Include(x => x.Alumno).FirstOrDefault(x => x.Id == idmaestro);
        }

        public override bool Validate(Docente docente)
        {
            if (docente.Clave <= 0)
            { 
                throw new Exception("Escribe el número de control del docente."); 
            }
            if (docente.Clave == 1234)
            { 
                throw new Exception("Este no es un número de control válido.");
            }
            if (string.IsNullOrWhiteSpace(docente.Nombre))
            { 
                throw new Exception("Escribe el nombre del docente."); 
            }
            if (string.IsNullOrWhiteSpace(docente.Contrasena))
            { 
                throw new Exception("Escribe la contraseña del docente."); 
            }
            if (docente.Clave.ToString().Length > 4)
            { 
                throw new Exception("El número de control no debe exceder los 4 dígitos."); 
            }
            if (docente.Clave.ToString().Length < 4)
            {
                throw new Exception("Minimo 4 caracteres para el número de control.");
            }
            return true;
        }
    }
}
