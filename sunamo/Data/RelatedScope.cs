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
        bool?[] states = null;

        public RelatedScope(int arraySize)
        {
            states = new bool?[arraySize];
        }

        public RelatedScope(bool?[] states)
        {
            this.states = states;
        }

        public List<FromTo> RangeFromState(bool def, bool b)
        {
            List<FromTo> foundedRanges = new List<FromTo>();
            // true - is in code block. false - in comment block
            bool previous = def;
            FromTo fromTo = new FromTo();
            fromTo.from = 0;

            for (int i = 0; i < states.Length; i++)
            {
                bool? state = states[i];
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
                        // I'm actually in code block
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
                fromTo.to = states.Length - 1 -1 +1;
                if (fromTo.from != fromTo.to)
                {
                    foundedRanges.Add(fromTo);
                }
            }

            return foundedRanges;
        }
    }
}
