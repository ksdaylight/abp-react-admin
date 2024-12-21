using Volo.Abp.DynamicProxy;

namespace Miwen.Abp.Idempotent;

public interface IIdempotentKeyNormalizer
{
    string NormalizeKey(IdempotentKeyNormalizerContext context);
}
