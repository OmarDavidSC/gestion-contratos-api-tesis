﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContrato.Dto
{
    public class UserLoginDto
    {
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
    }
}
