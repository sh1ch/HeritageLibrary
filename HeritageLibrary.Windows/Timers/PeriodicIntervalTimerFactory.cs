using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Timers;

public class PeriodicIntervalTimerFactory : IIntervalTimerFactory
{
	public IIntervalTimer Create(uint interval)
	{
		return new PeriodicIntervalTimer(interval);
	}
}
