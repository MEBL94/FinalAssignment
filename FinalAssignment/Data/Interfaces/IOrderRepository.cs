using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalAssignment.Models;

namespace FinalAssignment.Data.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
