using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Field
    {
        public State[,] _field;

        /// <summary>
        /// Create a new Field
        /// </summary>
        /// <param name="width">Width of the Field</param>
        /// <param name="height">Height of the Field</param>
        public Field(int width, int height)
        {
            _field = new State[width,height];

            for (int x = 0; x < _field.GetLength(0); x++ )
            {
                for (int y = 0; y < _field.GetLength(1); y++)
                {
                    _field[x,y] = State.EMPTY;
                }
            }
        }
    }
}
