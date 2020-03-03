using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Values
{
    public class RandomStatuses
    {
        public void SetStatusOfType(TypeOfMessage type)
        {
            ThisApp.SetStatus(type, type.ToString());
        }

        public void SetAllTypes()
        {
            foreach (var item in EnumHelper.GetValues<TypeOfMessage>())
            {
                SetStatusOfType(item);
            }
        }
    }
}