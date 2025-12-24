using Application.Exceptions;
using Domain.Interfaces;
using AutoMapper;
using Application.Handlers;
using Application.DTOs.Products;

namespace Application.Comands.CreateProductCommand
{
    public record CreateProductCommand(CreateProductDto Dto);
}
