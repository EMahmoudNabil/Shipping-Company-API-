using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Auth.Model;
public record LoginDTO
(  
        string Email,
        string Password
);