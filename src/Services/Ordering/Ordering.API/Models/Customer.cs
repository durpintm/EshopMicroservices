﻿using Ordering.API.Abstractions;

namespace Ordering.API.Models;

public class Customer : Entity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}

