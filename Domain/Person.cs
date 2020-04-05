﻿using System;

namespace Domain
{
    public class Person
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString() =>
            $"Id:{PersonId}, Name:{FirstName} {LastName}";
    }
}
