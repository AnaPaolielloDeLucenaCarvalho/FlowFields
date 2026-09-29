namespace FlowPathfinding
{
    // Cost of travelling across different types of terrain
    public enum TerrainCost : byte
    {
        FastGround = 1, // Downhill -> speed boost
        NormalGround = 2, // Normal -> flat ground
        UpHill = 4, // UpHill -> slightly harder to move
        Water = 10, // Water -> slower
        Mud = 15, // Mud -> very slow
        Fire = 50, // Fire -> extreme high cost (agents will avoid unless only way)
        BreakWall = 100, // Destructible wall -> agents will walk around if possible, but go through if have to
        Impassable = 255 // Impassable -> indestructible walls
    }
}
