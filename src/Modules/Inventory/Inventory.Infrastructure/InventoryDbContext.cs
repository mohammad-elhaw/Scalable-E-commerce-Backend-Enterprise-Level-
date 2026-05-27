using Application.Abstractions.Messaging;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure;

internal class InventoryDbContext(
    DbContextOptions<ModuleDbContext> options, 
    IDomainEventDispatcher dispatcher)
    : ModuleDbContext(options, dispatcher)
{

}
