using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Second.ViewModels;

public  class Base
{
    protected ToursContextFactory ContextFactory;
    protected Mapper Mapper;
    public Base()
    {
        ContextFactory = new ToursContextFactory();
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper = new Mapper(config);
    }
}