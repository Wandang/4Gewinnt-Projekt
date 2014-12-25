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
        public State[,] _field;

        /// <summary>
        /// Create a new Field
        /// </summary>
        /// <param name="width">Width of the Field</param>
        /// <param name="height">Height of the Field</param>
        public Field(int width, int height)
        {
            _field = new State[width,height];

            for (var x = 0; x < _field.GetLength(0); x++ )
            {
                for (var y = 0; y < _field.GetLength(1); y++)
                {
                    _field[x,y] = State.Empty;
                }
            }
        }

        public State Get(int y, int x)
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

        public void Set(int row, State player)
        {
            for (var y = _field.GetLength(1)-1; y > 0; y--)
            {
                if (_field[row, y] != State.Empty) continue;
                
                _field[row, y] = player;
                break;
            }
        }
    }
}
