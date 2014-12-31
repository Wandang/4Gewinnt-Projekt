using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Field
    {
        State[,] _field;

        public int Height
        {
            get { return _field.GetLength(1); }
        }

        public int Width
        {
            get { return _field.GetLength(0); }
        }

        /// <summary>
        /// Create a new Field
        /// </summary>
        /// <param name="width">Width of the Field</param>
        /// <param name="height">Height of the Field</param>
        public Field(int width, int height)
        {
            
            _field = new State[width,height];

            for (var x = 0; x < width; x++ )
            {
                for (var y = 0; y < height; y++)
                {
                    _field[x,y] = State.Empty; // Empty init our array
                }
            }
        }

        public State Get(int x, int y)
        {
            return _field[x, y];
        }

        public bool HasEmpty(int col)
        {
            for (var y = 0; y < _field.GetLength(1); y++)
            {
                if (_field[col, y] == State.Empty)
                {
                    return true;
                }
            }

            return false;
        }

        public int Set(int column, State player)
        {
            for (var y = _field.GetLength(1)-1; y >= 0; y--)
            {
                if (_field[column, y] != State.Empty) continue;
                
                _field[column, y] = player;
                return y;
            }

            return -1;
        }
    }
}
