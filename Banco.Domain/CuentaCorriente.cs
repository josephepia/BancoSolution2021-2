﻿using Banco.Domain.CuentaBancaria;
using System;

namespace Banco.Domain
{
    public class CuentaCorriente : CuentaBancariaBase
    {
        public decimal Sobregiro { get; private set; }

        public CuentaCorriente(string numero, string nombre, decimal sobregiro) :
            base(numero, nombre, 100000)
        {
            Sobregiro = -sobregiro;
        }


        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            var cuatroPorMil = valorRetiro * 4 / 1000;
            var nuevoSaldoTemporal = Saldo - valorRetiro - cuatroPorMil;
            if (nuevoSaldoTemporal > Sobregiro)
            {
                AddMovimientoDisminuyeSaldo(valorRetiro, fecha, "RETIRO");
                AddMovimientoDisminuyeSaldo(cuatroPorMil, fecha, "IMP4XMIL");
                return $"Su Nuevo Saldo es de {Saldo:c2} pesos m/c";
            }
            throw new NotImplementedException();
        }

    }


}
