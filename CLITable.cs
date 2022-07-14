using System.Text;

namespace CLITables
{
    public class CLITable
    {
        readonly int cols, colsWidth, lineSize;
        readonly char colSeparator, rowSeparator;
        readonly Alignment alignment;
        readonly StringBuilder stringBuilder;

        /// <summary>
        /// Instancia un generador de tablas básico.
        /// </summary>
        /// <param name="maxWidth">Máximo de caracteres que puede ocupar la tabla.</param>
        /// <param name="cols">Número de columnas que debe tener la tabla.</param>
        /// <param name="tableConfig">Objeto de configuración de la tabla (<see cref="TableConfig"/>). Si no se aporta utiliza '|' como separador de columnas, '-' como separador de filas y alinea el contenido al centro de la celda.</param>
        /// <exception cref="Exception">No se puede construir la tabla: ancho o número de columnas negativo o demasiado corto.</exception>
        public CLITable(int maxWidth, int cols, TableConfig? tableConfig = null)
        {
            if (cols > 0 && maxWidth > cols + 2)
            {
                tableConfig ??= new();
                this.cols = cols;
                colSeparator = tableConfig.colSeparator;
                rowSeparator = tableConfig.rowSeparator;
                colsWidth = (maxWidth - cols) / cols;
                lineSize = colsWidth * cols + cols + 1;
                alignment = tableConfig.alignment;
                stringBuilder = new();
            }
            else
            {
                throw new Exception($"No puedo construir una tabla con un ancho máximo de {maxWidth} caracteres y {cols} columnas.");
            }
        }

        /// <summary>
        /// Genera una línea de separación para la tabla descrita.
        /// </summary>
        /// <returns>Una cadena que repite el caracter de separación entre filas hasta obtener el ancho adecuado.</returns>
        public string NewLine()
        {
            return new string(rowSeparator, lineSize);
        }
        /// <summary>
        /// Imprime una línea de separación generada por <see cref="NewLine"/>.
        /// </summary>
        public CLITable PrintLine()
        {
            string line = NewLine();
            Console.WriteLine(line);
            return this;
        }
        /// <summary>
        /// Genera una nueva fila para la tabla descrita a partir de un <c>string[]</c> con el contenido de las columnas.
        /// </summary>
        /// <param name="columns"><c>string[]</c> con tamaño igual a número de columnas indicado que guarda el contenido de los distintos campos.</param>
        /// <returns>Una cadena con la nueva fila que contiene los campos proporcionados por argumentos.</returns>
        /// <exception cref="Exception">El número de columnas de la tabla y la cantidad de campos proporcionados no coinciden.</exception>
        public string NewRow(params string[] columns)
        {
            if (columns.Length == cols)
            {
                stringBuilder.Clear();
                stringBuilder.Append(colSeparator);

                foreach (string column in columns)
                {
                    string field = Align(column);
                    stringBuilder.Append(field);
                    stringBuilder.Append(colSeparator);
                }

                return stringBuilder.ToString();
            }
            else
            {
                throw new Exception($"Se esperaba un array de tamaño igual a {cols}");
            }
        }
        /// <summary>
        /// Imprime una nueva fila generada llamando al método <see cref="NewRow"/> con los mismos argumentos.
        /// </summary>
        /// <param name="columns">Una cadena con la nueva fila que contiene los campos proporcionados por argumentos.</param>
        public CLITable PrintRow(params string[] columns)
        {
            string newColumn = NewRow(columns);
            Console.WriteLine(newColumn);
            return this;
        }

        string Align(string fieldContent)
        {
            if (string.IsNullOrEmpty(fieldContent))
            {
                return new string(' ', colsWidth);
            }

            if (fieldContent.Length > colsWidth)
            {
                return fieldContent.Substring(0, colsWidth - 3) + "...";
            }

            switch (alignment)
            {
                case Alignment.Left:
                    return AlignLeft(fieldContent);
                case Alignment.Center:
                    return AlignCentre(fieldContent);
                case Alignment.Right:
                    return AlignRight(fieldContent);
                default:
                    throw new Exception($"El campo {nameof(alignment)} no se encuentra definido.");
            }
        }
        string AlignLeft(string fieldContent)
        {
            return fieldContent.PadRight(colsWidth);
        }
        string AlignCentre(string fieldContent)
        {
            int padRight = colsWidth - (colsWidth - fieldContent.Length) / 2;

            fieldContent = fieldContent
                .PadRight(padRight)
                .PadLeft(colsWidth);

            return fieldContent;
        }
        string AlignRight(string fieldContent)
        {
            return fieldContent.PadLeft(colsWidth);
        }
    }
}