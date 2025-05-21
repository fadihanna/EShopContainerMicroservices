using System;
using System.Collections.Generic;

namespace BuildingBlocks.Models;

public record FeesResponseModel(
    string Status,
    string StatusText,
    string DateTime,
    double Amount,
    double Fees,
    double TotalFees
    );
