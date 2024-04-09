public static class DamageManager
{
    public static void Damage(Health targetHealth, float damage)
    {
        targetHealth.TakeDamage(damage);
    }
}
