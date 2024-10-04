using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchivoVisual.Models
{
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Nota { get; set; }
        public char Genero { get; set; }

        public Persona(string nombre, int edad, int nota, char genero)
        {
            Nombre = nombre;
            Edad = edad;
            Nota = nota;
            Genero = genero;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Edad: {Edad}, Nota: {Nota}, Género: {Genero}";
        }
    }
}
