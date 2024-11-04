﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Model
{
    public class Service
    {
        public Service(string from, string to, int trainNumber, int delayAmount, string trainType)
        {
            Id = Guid.NewGuid().ToString();
            From = from;
            To = to;
            TrainNumber = trainNumber;
            DelayAmount = delayAmount;
            TrainType = trainType;
        }

        public Service()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int TrainNumber { get; set; }

        public int DelayAmount { get; set; }

        public string TrainType { get; set; }

    }
}