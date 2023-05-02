using System;
using Microsoft.EntityFrameworkCore;

namespace Second.ViewModels;

public  class Base
{
    protected ToursContextFactory ContextFactory;

    public Base()
    {
        ContextFactory = new ToursContextFactory();
    }
}