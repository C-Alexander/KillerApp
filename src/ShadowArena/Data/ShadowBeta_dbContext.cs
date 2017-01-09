using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShadowArena.Models
{
    public partial class ShadowBeta_dbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@" Server=tcp:sagamedbserver.database.windows.net,1433;Initial Catalog=ShadowBeta_db;Persist Security Info=False;User ID=sa_admin;Password= P-5:.Z:bRHu}?NgQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.OwningPlayerid).HasColumnName("owningPlayerid");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Character)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacter665903");

                entity.HasOne(d => d.OwningPlayer)
                    .WithMany(p => p.Character)
                    .HasForeignKey(d => d.OwningPlayerid)
                    .HasConstraintName("FKCharacter420514");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.Character)
                    .HasForeignKey(d => d.Statid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacter141193");
            });

            modelBuilder.Entity<CharacterItem>(entity =>
            {
                entity.HasKey(e => new { e.OwningCharacterid, e.OwnedItemid })
                    .HasName("PK__Characte__5EE4219A3DA871E8");

                entity.Property(e => e.OwningCharacterid).HasColumnName("owningCharacterid");

                entity.Property(e => e.OwnedItemid).HasColumnName("ownedItemid");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Equipped).HasColumnName("equipped");

                entity.HasOne(d => d.OwnedItem)
                    .WithMany(p => p.CharacterItem)
                    .HasForeignKey(d => d.OwnedItemid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacterI489001");

                entity.HasOne(d => d.OwningCharacter)
                    .WithMany(p => p.CharacterItem)
                    .HasForeignKey(d => d.OwningCharacterid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacterI329572");
            });

            modelBuilder.Entity<CharacterSkill>(entity =>
            {
                entity.HasKey(e => new { e.Characterid, e.Skillid })
                    .HasName("PK__Characte__98B68EA1EB24099C");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.CharacterSkill)
                    .HasForeignKey(d => d.Characterid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacterS268350");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.CharacterSkill)
                    .HasForeignKey(d => d.Skillid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKCharacterS291395");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)");

                entity.HasOne(d => d.Stat)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.Statid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKClass835670");
            });

            modelBuilder.Entity<GlobalSetting>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__GlobalSe__DFD83CAEB0CDE542");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasColumnName("dataType")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EffectingStatid).HasColumnName("effectingStatid");

                entity.Property(e => e.MaximumLevel).HasColumnName("maximumLevel");

                entity.Property(e => e.MinimumLevel).HasColumnName("minimumLevel");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SoldInShopid).HasColumnName("soldInShopid");

                entity.HasOne(d => d.EffectingStat)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.EffectingStatid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKItem616611");

                entity.HasOne(d => d.SoldInShop)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.SoldInShopid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKItem91559");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Experience)
                    .HasColumnName("experience")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasColumnName("passWord")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<PlayerSession>(entity =>
            {
                entity.HasKey(e => new { e.Playerid, e.Sessionid })
                    .HasName("PK__PlayerSe__D6D0110A0C7B9B31");

                entity.Property(e => e.Sessionid).HasColumnType("varchar(256)");

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerSession)
                    .HasForeignKey(d => d.Playerid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKPlayerSess994092");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("userAgent")
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.PriceModifier).HasColumnName("priceModifier");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoolDown)
                    .HasColumnName("coolDown")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.HealthCost)
                    .HasColumnName("healthCost")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ManaCost)
                    .HasColumnName("manaCost")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("'unknown'");

                entity.Property(e => e.UpgradesFromSkill).HasColumnName("upgradesFromSkill");

                entity.Property(e => e.WarmUp)
                    .HasColumnName("warmUp")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.UpgradesFromSkillNavigation)
                    .WithMany(p => p.InverseUpgradesFromSkillNavigation)
                    .HasForeignKey(d => d.UpgradesFromSkill)
                    .HasConstraintName("SkillUpgrade");
            });

            modelBuilder.Entity<SkillClass>(entity =>
            {
                entity.HasKey(e => new { e.Skillid, e.Classid })
                    .HasName("PK__SkillCla__43165724055CCDBF");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.SkillClass)
                    .HasForeignKey(d => d.Classid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSkillClass398388");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.SkillClass)
                    .HasForeignKey(d => d.Skillid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKSkillClass117124");
            });

            modelBuilder.Entity<Stat>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attack).HasColumnName("attack");

                entity.Property(e => e.Defense).HasColumnName("defense");

                entity.Property(e => e.Intelligence).HasColumnName("intelligence");

                entity.Property(e => e.MagicAttack).HasColumnName("magicAttack");

                entity.Property(e => e.MagicDefence).HasColumnName("magicDefence");

                entity.Property(e => e.Vitality).HasColumnName("vitality");
            });
        }

        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<CharacterItem> CharacterItem { get; set; }
        public virtual DbSet<CharacterSkill> CharacterSkill { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSetting { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerSession> PlayerSession { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<SkillClass> SkillClass { get; set; }
        public virtual DbSet<Stat> Stat { get; set; }
    }
}