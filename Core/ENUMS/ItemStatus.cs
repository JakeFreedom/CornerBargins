using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ENUMS
{
    public enum ItemStatus
    {
        ITEM_VISIBLE = 1,
        ITEM_OBSOLETE = 2,
        ITEM_SUPERSEDED = 4,
        ITEM_LOCKED = 8,
        ITEM_SOLD = 16
    }
}
