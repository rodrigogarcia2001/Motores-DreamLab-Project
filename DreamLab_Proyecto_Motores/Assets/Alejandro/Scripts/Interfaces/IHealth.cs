public interface IHealth {
    public void updateHealthBar(int damage);
    public void takeDamaged(int damage);
    public int getHealth();
    public void addHealth(int health);
    public int getMaxHealth();
    public bool isDead();
}
