using System;
using System.Collections.Generic;

namespace SmartSoftware.DependencyInjection;

public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
{

}
