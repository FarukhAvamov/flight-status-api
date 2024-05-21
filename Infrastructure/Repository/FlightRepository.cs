using Domain.Domain.AggregateModels.FlightAggregate.cs;
using Domain.Domain.SeedWork;
using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repository
{
    public class FlightRepository : IFlightRepository
    {

        private readonly AirContext _context;
        

        public FlightRepository(AirContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public Flight Add(Flight buyer) => _context.Add(buyer).Entity;
        public Flight Update(Flight buyer) => _context.Update(buyer).Entity;
   

        public async Task<IReadOnlyCollection<Flight>> GetAllAsync()
        {
            var flights= new ReadOnlyCollection<Flight>(await _context.Flights.ToListAsync());
            return flights;
        } 

        public async Task<Flight?> FindByIdAsync(int id)
        {
            return await _context.Flights
            .Where(b => b.ID == id)
            .SingleOrDefaultAsync();
        }
    }
}
