using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    /// <summary>
    /// null is neutral(if has before and after same state, is considered as this state)
    /// </summary>
    public class RelatedScope
    {
        private bool?[] _states = null;

        public RelatedScope(int arraySize)
        {
            _states = new bool?[arraySize];
        }

        public RelatedScope(bool?[] states)
        {
            _states = states;
        }

        /// <summary>
        /// Is used for deleting regions blocks. All lines between must dont exists or be empty
        /// </summary>
        /// <param name="def"></param>
        /// <param name="b"></param>
        public List<FromTo> RangeFromStateSimple(List<int> startIndexes)
        {
            List<FromTo> foundedRanges = new List<FromTo>();

            bool insideRegion = false;
            FromTo fromTo = new FromTo();

            for (int i = 0; i < _states.Length; i++)
            {
                bool? b = _states[i];
                if (b.HasValue)
                {
                    if (b.Value)
                    {
                        if (insideRegion)
                        {
                            if (startIndexes.Contains(i))
                            {
                                fromTo.from = i;
                            }
                            else
                            {
                                insideRegion = false;
                                fromTo.to = i;
                                foundedRanges.Add(fromTo);
                            }
                        }
                        else
                        {
                            insideRegion = true;

                            fromTo = new FromTo();
                            fromTo.from = i;
                        }
                    }
                }
            }

            return foundedRanges;
        }

        /// <summary>
        /// Was used for deleting comments. Returns serie only when is all lines between is comments
        /// </summary>
        /// <param name="def"></param>
        /// <param name="b"></param>
        public List<FromTo> RangeFromState(bool def, bool b)
        {
            List<FromTo> foundedRanges = new List<FromTo>();
            // true - is in code block. false - in non-code block
            bool previous = def;
            FromTo fromTo = new FromTo();
            fromTo.from = 0;

            for (int i = 0; i < _states.Length; i++)
            {
                bool? state = _states[i];
                // If line have some content
                if (state.HasValue)
                {
                    // and it's code
                    if (!state.Value)
                    {
                        // ... and actually I'm in comment block
                        if (!previous)
                        {
                            fromTo.to = i - 1;
                            if (fromTo.from != fromTo.to)
                            {
                                foundedRanges.Add(fromTo);
                            }
                            previous = true;
                        }
                    }
                    // its comment!
                    else
                    {
                        // I'm actually in non-code block
                        if (previous)
                        {
                            fromTo = new FromTo();
                            fromTo.from = i;
                            previous = false;
                        }
                    }
                }
            }

            if (!previous)
            {
                fromTo.to = _states.Length - 1 - 1 + 1;
                if (fromTo.from != fromTo.to)
                {
                    foundedRanges.Add(fromTo);
                }
            }

            return foundedRanges;
        }
    }
}