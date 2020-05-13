using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TextAnalyser.Models
{
    public partial class mph_uaContext : DbContext
    {
        public mph_uaContext()
        {
        }

        public mph_uaContext(DbContextOptions<mph_uaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abbreviatury> Abbreviatury { get; set; }
        public virtual DbSet<Accent> Accent { get; set; }
        public virtual DbSet<AccentsClass> AccentsClass { get; set; }
        public virtual DbSet<Alphadigit> Alphadigit { get; set; }
        public virtual DbSet<Flexes> Flexes { get; set; }
        public virtual DbSet<Gr> Gr { get; set; }
        public virtual DbSet<Indents> Indents { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }
        public virtual DbSet<MinorAcc> MinorAcc { get; set; }
        public virtual DbSet<Nom> Nom { get; set; }
        public virtual DbSet<Parts> Parts { get; set; }
        public virtual DbSet<Shortening> Shortening { get; set; }
        public virtual DbSet<TranscIrregular> TranscIrregular { get; set; }
        public virtual DbSet<TypRefl> TypRefl { get; set; }
        public virtual DbSet<WordList> WordList { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlite("Data Source=C:/Users/Myroslav/Desktop/chekerDb/mph_ua.db;");
                optionsBuilder.UseSqlite("Data Source=C:/Users/Myroslav/Desktop/mph_ua.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abbreviatury>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("abbreviatury");

                entity.Property(e => e.Col002).HasColumnType("nvarchar(255)");

                entity.Property(e => e.Col003).HasColumnType("nvarchar(255)");

                entity.Property(e => e.NomOld).HasColumnName("nom_old");
            });

            modelBuilder.Entity<Accent>(entity =>
            {
                entity.ToTable("accent");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccentType)
                    .HasColumnName("accent_type")
                    .HasColumnType("smallint");

                entity.Property(e => e.Gram)
                    .HasColumnName("gram")
                    .HasColumnType("smallint");

                entity.Property(e => e.Indent1)
                    .HasColumnName("indent1")
                    .HasColumnType("smallint");

                entity.Property(e => e.Indent2)
                    .HasColumnName("indent2")
                    .HasColumnType("smallint");

                entity.Property(e => e.Indent3)
                    .HasColumnName("indent3")
                    .HasColumnType("smallint");

                entity.Property(e => e.Indent4)
                    .HasColumnName("indent4")
                    .HasColumnType("smallint");

                entity.Property(e => e.Xmpl)
                    .HasColumnName("xmpl")
                    .HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.AccentTypeNavigation)
                    .WithMany(p => p.Accent)
                    .HasForeignKey(d => d.AccentType);
            });

            modelBuilder.Entity<AccentsClass>(entity =>
            {
                entity.ToTable("accents_class");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint")
                    .ValueGeneratedNever();

                entity.Property(e => e.PartDesc)
                    .HasColumnName("part_desc")
                    .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<Alphadigit>(entity =>
            {
                entity.HasKey(e => new { e.Lang, e.Alpha, e.Ls });

                entity.ToTable("alphadigit");

                entity.Property(e => e.Lang).HasColumnName("lang");

                entity.Property(e => e.Alpha)
                    .HasColumnName("alpha")
                    .HasColumnType("nvarchar(10)");

                entity.Property(e => e.Ls)
                    .HasColumnName("ls")
                    .HasColumnType("smallint");

                entity.Property(e => e.Digit)
                    .IsRequired()
                    .HasColumnName("digit")
                    .HasColumnType("nvarchar(10)");
            });

            modelBuilder.Entity<Flexes>(entity =>
            {
                entity.ToTable("flexes");

                entity.HasIndex(e => e.Field2)
                    .HasName("flexes_IX_flex");

                entity.HasIndex(e => e.Type)
                    .HasName("flexes_IX_flex_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Digit)
                    .HasColumnName("digit")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Field2).HasColumnName("field2");

                entity.Property(e => e.Flex)
                    .HasColumnName("flex")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint");

                entity.Property(e => e.Xmpl)
                    .HasColumnName("xmpl")
                    .HasColumnType("nvarchar(255)");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Flexes)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Gr>(entity =>
            {
                entity.ToTable("gr");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint")
                    .ValueGeneratedNever();

                entity.Property(e => e.Field10)
                    .HasColumnName("field10")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field11)
                    .HasColumnName("field11")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field12)
                    .HasColumnName("field12")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field13)
                    .HasColumnName("field13")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field14)
                    .HasColumnName("field14")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field15)
                    .HasColumnName("field15")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field16)
                    .HasColumnName("field16")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field17)
                    .HasColumnName("field17")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field18)
                    .HasColumnName("field18")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field19)
                    .HasColumnName("field19")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field20)
                    .HasColumnName("field20")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field21)
                    .HasColumnName("field21")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field22)
                    .HasColumnName("field22")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field23)
                    .HasColumnName("field23")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field24)
                    .HasColumnName("field24")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field25)
                    .HasColumnName("field25")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field26)
                    .HasColumnName("field26")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field27)
                    .HasColumnName("field27")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field28)
                    .HasColumnName("field28")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field29)
                    .HasColumnName("field29")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field30)
                    .HasColumnName("field30")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field31)
                    .HasColumnName("field31")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field32)
                    .HasColumnName("field32")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field4)
                    .HasColumnName("field4")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field5)
                    .HasColumnName("field5")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field6)
                    .HasColumnName("field6")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field7)
                    .HasColumnName("field7")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field8)
                    .HasColumnName("field8")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Field9)
                    .HasColumnName("field9")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.PartOfSpeech)
                    .HasColumnName("part_of_speech")
                    .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<Indents>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.ToTable("indents");

                entity.HasIndex(e => e.Comment)
                    .HasName("indents_IX_indent_1");

                entity.HasIndex(e => e.Type)
                    .HasName("indents_IX_indent");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Field3).HasColumnName("field3");

                entity.Property(e => e.Field4).HasColumnName("field4");

                entity.Property(e => e.GrId)
                    .HasColumnName("gr_id")
                    .HasColumnType("smallint");

                entity.Property(e => e.Indent).HasColumnName("indent");

                entity.HasOne(d => d.Gr)
                    .WithMany(p => p.Indents)
                    .HasForeignKey(d => d.GrId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Lang>(entity =>
            {
                entity.HasKey(e => e.Pref);

                entity.ToTable("lang");

                entity.Property(e => e.Pref)
                    .HasColumnName("pref")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.IdLang).HasColumnName("id_lang");

                entity.Property(e => e.Lang1)
                    .IsRequired()
                    .HasColumnName("lang")
                    .HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<MinorAcc>(entity =>
            {
                entity.HasKey(e => e.NomOld);

                entity.ToTable("minor_acc");

                entity.Property(e => e.NomOld)
                    .HasColumnName("nom_old")
                    .ValueGeneratedNever();

                entity.Property(e => e.Double1)
                    .HasColumnName("double1")
                    .HasColumnType("smallint");

                entity.Property(e => e.Double2)
                    .HasColumnName("double2")
                    .HasColumnType("smallint");

                entity.Property(e => e.Occur1)
                    .HasColumnName("occur1")
                    .HasColumnType("smallint");

                entity.Property(e => e.Occur2)
                    .HasColumnName("occur2")
                    .HasColumnType("smallint");

                entity.Property(e => e.Occur3)
                    .HasColumnName("occur3")
                    .HasColumnType("smallint");

                entity.Property(e => e.WordE1)
                    .HasColumnName("word_e1")
                    .HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<Nom>(entity =>
            {
                entity.HasKey(e => e.NomOld);

                entity.ToTable("nom");

                entity.HasIndex(e => e.Digit)
                    .HasName("nom_IX_nom_1");

                entity.HasIndex(e => e.Field2)
                    .HasName("nom_IX_nom_4");

                entity.HasIndex(e => e.Field7)
                    .HasName("nom_IX_nom_5");

                entity.HasIndex(e => e.NomOld)
                    .HasName("nom_IX_nom_3")
                    .IsUnique();

                entity.HasIndex(e => e.Own)
                    .HasName("nom_IX_nom_9");

                entity.HasIndex(e => e.Part)
                    .HasName("nom_IX_nom_6");

                entity.HasIndex(e => e.Reestr)
                    .HasName("nom_IX_nom_10");

                entity.HasIndex(e => e.Reverse)
                    .HasName("nom_IX_nom_2");

                entity.HasIndex(e => e.Type)
                    .HasName("nom_IX_nom_7");

                entity.Property(e => e.NomOld)
                    .HasColumnName("nom_old")
                    .ValueGeneratedNever();

                entity.Property(e => e.Accent)
                    .HasColumnName("accent")
                    .HasColumnType("smallint");

                entity.Property(e => e.Digit)
                    .IsRequired()
                    .HasColumnName("digit")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Field2)
                    .HasColumnName("field2")
                    .HasColumnType("smallint");

                entity.Property(e => e.Field5)
                    .HasColumnName("field5")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Field6)
                    .HasColumnName("field6")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Field7)
                    .HasColumnName("field7")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Isdel)
                    .IsRequired()
                    .HasColumnName("isdel")
                    .HasColumnType("bit");

                entity.Property(e => e.Isproblem)
                    .HasColumnName("isproblem")
                    .HasColumnType("bit");

                entity.Property(e => e.Own)
                    .HasColumnName("own")
                    .HasColumnType("smallint");

                entity.Property(e => e.Part)
                    .HasColumnName("part")
                    .HasColumnType("smallint");

                entity.Property(e => e.Reestr)
                    .IsRequired()
                    .HasColumnName("reestr")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Reverse)
                    .IsRequired()
                    .HasColumnName("reverse")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.SupplAccent)
                    .HasColumnName("suppl_accent")
                    .HasColumnType("bit");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint");

                entity.HasOne(d => d.AccentNavigation)
                    .WithMany(p => p.Nom)
                    .HasForeignKey(d => d.Accent);

                entity.HasOne(d => d.PartNavigation)
                    .WithMany(p => p.Nom)
                    .HasForeignKey(d => d.Part)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Nom)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Parts>(entity =>
            {
                entity.ToTable("parts");

                entity.HasIndex(e => e.Com)
                    .HasName("parts_IX_Parts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ac)
                    .HasColumnName("ac")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Adjekt).HasColumnName("adjekt");

                entity.Property(e => e.Com)
                    .HasColumnName("com")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.GrId)
                    .HasColumnName("gr_id")
                    .HasColumnType("smallint");

                entity.Property(e => e.Istota).HasColumnName("istota");

                entity.Property(e => e.Mnozh).HasColumnName("mnozh");

                entity.Property(e => e.Part)
                    .HasColumnName("part")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Rid).HasColumnName("rid");

                entity.Property(e => e.Vid).HasColumnName("vid");

                entity.HasOne(d => d.Gr)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.GrId);
            });

            modelBuilder.Entity<Shortening>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("shortening");

                entity.HasIndex(e => e.Shortening1)
                    .HasName("shortening_IX_Shortening");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Shortening1)
                    .HasColumnName("shortening")
                    .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<TranscIrregular>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("transc_irregular");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PositionDel1).HasColumnType("smallint");

                entity.Property(e => e.PositionDel2).HasColumnType("smallint");

                entity.Property(e => e.Reestr)
                    .IsRequired()
                    .HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<TypRefl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("typ_refl");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint");
            });

            modelBuilder.Entity<WordList>(e =>
            {
                e.HasKey(x => x.Id);

                e.ToTable("wordlist");

                e.Property(x => x.Word).HasColumnName("word").HasColumnType("varchar(255)");

                e.HasOne(d => d.Nom)
                    .WithMany(p => p.WordList)
                    .HasForeignKey(d => d.NomId)
                    .HasConstraintName("nom_id");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
