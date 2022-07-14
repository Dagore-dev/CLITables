using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLITables
{
    public class TableConfig
    {
        public char colSeparator;
        public char rowSeparator;
        public Alignment alignment;

        /// <summary>
        /// Objeto de configuración para <see cref="CLITable"/>.
        /// </summary>
        /// <param name="colSeparator">Caracter de separación entre columnas. Por defecto '|'.</param>
        /// <param name="rowSeparator">Caracter de separación entre filas. Por defecto '-'.</param>
        /// <param name="alignment">Un enumerado <see cref="Alignment"/> que permite escoger cómo alinear el contenido de cada celda: A la izquierda, centro o derecha.</param>
        public TableConfig(char colSeparator = '|', char rowSeparator = '-', Alignment alignment = Alignment.Center)
        {
            this.colSeparator = colSeparator;
            this.rowSeparator = rowSeparator;
            this.alignment = alignment;
        }
    }
}
