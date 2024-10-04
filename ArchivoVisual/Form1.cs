using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArchivoVisual.Models;

namespace ArchivoVisual
{
    public partial class Form1 : Form
    {
        private const string FilePath = "datos.dat"; // Ruta del archivo binario
        private int personCount = 0; // Contador de personas
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Verificar si ya se han guardado 3 personas
            if (personCount < 3)
            {
                // Crear una nueva instancia de Persona con los datos ingresados
                Persona persona = new Persona(
                    tbName.Text,
                    int.Parse(tbAge.Text),
                    int.Parse(tbScore.Text),
                    char.Parse(tbGender.Text)
                );

                // Guardar los datos en el archivo binario
                GuardarPersona(persona);

                // Incrementar el contador de personas
                personCount++;

                // Limpiar los TextBox
                tbName.Clear();
                tbAge.Clear();
                tbScore.Clear();
                tbGender.Clear();

                // Si ya se han ingresado 3 personas, deshabilitar el botón de guardar
                if (personCount == 3)
                {
                    btnSave.Enabled = false; // Deshabilitar el botón "Guardar"
                    MessageBox.Show("Se ha alcanzado el límite de 3 personas.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void GuardarPersona(Persona persona)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(persona.Nombre.Length); // Escribir longitud del nombre
                writer.Write(persona.Nombre.ToCharArray()); // Escribir el nombre
                writer.Write(persona.Edad); // Escribir la edad
                writer.Write(persona.Nota); // Escribir la nota
                writer.Write(persona.Genero); // Escribir el género
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            lbShow.Items.Clear(); // Limpiar la lista de datos previos

            // Leer las personas del archivo binario
            var personas = LeerPersonas();

            // Mostrar cada persona en el ListBox
            foreach (var persona in personas)
            {
                lbShow.Items.Add(persona.ToString());
            }
        }
        // Función para leer los datos del archivo binario
        private Persona[] LeerPersonas()
        {
            if (!File.Exists(FilePath)) return new Persona[0]; // Si el archivo no existe, retornar un array vacío

            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                var personas = new System.Collections.Generic.List<Persona>();

                while (fs.Position != fs.Length)
                {
                    // Leer la longitud del nombre
                    int nameLength = reader.ReadInt32();
                    // Leer el nombre
                    char[] nameArray = reader.ReadChars(nameLength);
                    string nombre = new string(nameArray);

                    // Leer el resto de los datos
                    int edad = reader.ReadInt32();
                    int nota = reader.ReadInt32();
                    char genero = reader.ReadChar();

                    // Añadir la persona a la lista
                    personas.Add(new Persona(nombre, edad, nota, genero));
                }

                return personas.ToArray(); // Retornar el array de personas
            }
        }
    }
}
