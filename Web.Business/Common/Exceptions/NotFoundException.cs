using EucProject.Resources;

namespace Web.Business.Common.Exceptions;
public class NotFoundException : Exception
{
	public NotFoundException(string name)
	  : base(string.Format(AppRes.EntityNotFound, name))
	{
	}
}
