﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? EditedDate { get; set; }

        public DateTime? RemovedDate { get; set; }
    }
}
