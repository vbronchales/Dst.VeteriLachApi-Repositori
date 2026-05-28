using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Infrastructure.Data;

public partial class VeteriLachDbContext : DbContext
{
    public VeteriLachDbContext()
    {
    }

    public VeteriLachDbContext(DbContextOptions<VeteriLachDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgnAgendesVisible> AgnAgendesVisibles { get; set; }

    public virtual DbSet<AgnAgendum> AgnAgenda { get; set; }

    public virtual DbSet<AgnAvi> AgnAvis { get; set; }

    public virtual DbSet<AgnAvisAgendum> AgnAvisAgenda { get; set; }

    public virtual DbSet<AgnCitum> AgnCita { get; set; }

    public virtual DbSet<AgnColor> AgnColors { get; set; }

    public virtual DbSet<AgnConfUsuari> AgnConfUsuaris { get; set; }

    public virtual DbSet<AgnFestiu> AgnFestius { get; set; }

    public virtual DbSet<AgnHoraDisponible> AgnHoraDisponibles { get; set; }

    public virtual DbSet<ArtArticle> ArtArticles { get; set; }

    public virtual DbSet<ArtCaixaArticle> ArtCaixaArticles { get; set; }

    public virtual DbSet<ArtConfiguracio> ArtConfiguracios { get; set; }

    public virtual DbSet<ArtFamilium> ArtFamilia { get; set; }

    public virtual DbSet<ArtInventari> ArtInventaris { get; set; }

    public virtual DbSet<ArtMagatzem> ArtMagatzems { get; set; }

    public virtual DbSet<ArtMarca> ArtMarcas { get; set; }

    public virtual DbSet<ArtMoviment> ArtMoviments { get; set; }

    public virtual DbSet<ArtMovimentInventari> ArtMovimentInventaris { get; set; }

    public virtual DbSet<ArtRelArticleMagatzem> ArtRelArticleMagatzems { get; set; }

    public virtual DbSet<ArtTraspa> ArtTraspas { get; set; }

    public virtual DbSet<FacAcompte> FacAcomptes { get; set; }

    public virtual DbSet<FacAlbara> FacAlbaras { get; set; }

    public virtual DbSet<FacAreaNegoci> FacAreaNegocis { get; set; }

    public virtual DbSet<FacArticle> FacArticles { get; set; }

    public virtual DbSet<FacArticleAlbara> FacArticleAlbaras { get; set; }

    public virtual DbSet<FacArticleCancelat> FacArticleCancelats { get; set; }

    public virtual DbSet<FacArticleComandum> FacArticleComanda { get; set; }

    public virtual DbSet<FacArticleFactura> FacArticleFacturas { get; set; }

    public virtual DbSet<FacArticlePressupost> FacArticlePressuposts { get; set; }

    public virtual DbSet<FacArticleReservat> FacArticleReservats { get; set; }

    public virtual DbSet<FacArticleVenut> FacArticleVenuts { get; set; }

    public virtual DbSet<FacCaixa> FacCaixas { get; set; }

    public virtual DbSet<FacCentreCost> FacCentreCosts { get; set; }

    public virtual DbSet<FacClient> FacClients { get; set; }

    public virtual DbSet<FacComandum> FacComanda { get; set; }

    public virtual DbSet<FacCompteBanc> FacCompteBancs { get; set; }

    public virtual DbSet<FacConfiguracio> FacConfiguracios { get; set; }

    public virtual DbSet<FacDespesa> FacDespesas { get; set; }

    public virtual DbSet<FacEmpresa> FacEmpresas { get; set; }

    public virtual DbSet<FacEntitatBancarium> FacEntitatBancaria { get; set; }

    public virtual DbSet<FacFactura> FacFacturas { get; set; }

    public virtual DbSet<FacFacturaClient> FacFacturaClients { get; set; }

    public virtual DbSet<FacFacturaProveidor> FacFacturaProveidors { get; set; }

    public virtual DbSet<FacFamilium> FacFamilia { get; set; }

    public virtual DbSet<FacIva> FacIvas { get; set; }

    public virtual DbSet<FacIvaFactura> FacIvaFacturas { get; set; }

    public virtual DbSet<FacNotificacioClient> FacNotificacioClients { get; set; }

    public virtual DbSet<FacPerfilTarifa> FacPerfilTarifas { get; set; }

    public virtual DbSet<FacPeriodica> FacPeriodicas { get; set; }

    public virtual DbSet<FacPressupost> FacPressuposts { get; set; }

    public virtual DbSet<FacProveidor> FacProveidors { get; set; }

    public virtual DbSet<FacReglaTarifa> FacReglaTarifas { get; set; }

    public virtual DbSet<FacRelAreaNegociCentreCost> FacRelAreaNegociCentreCosts { get; set; }

    public virtual DbSet<FacRelArticleProveidor> FacRelArticleProveidors { get; set; }

    public virtual DbSet<FacRelPerfilTarifaFamilium> FacRelPerfilTarifaFamilia { get; set; }

    public virtual DbSet<FacRelReglaTarifaArticle> FacRelReglaTarifaArticles { get; set; }

    public virtual DbSet<FacRelTarifaFamilium> FacRelTarifaFamilia { get; set; }

    public virtual DbSet<FacTancament> FacTancaments { get; set; }

    public virtual DbSet<FacTarifa> FacTarifas { get; set; }

    public virtual DbSet<FacTermini> FacTerminis { get; set; }

    public virtual DbSet<FacTipusTarifa> FacTipusTarifas { get; set; }

    public virtual DbSet<FacTraspa> FacTraspas { get; set; }

    public virtual DbSet<FacVendum> FacVenda { get; set; }

    public virtual DbSet<FacVenedor> FacVenedors { get; set; }

    public virtual DbSet<HosArticleHospital> HosArticleHospitals { get; set; }

    public virtual DbSet<HosArxiuVisitum> HosArxiuVisita { get; set; }

    public virtual DbSet<HosBaixa> HosBaixas { get; set; }

    public virtual DbSet<HosBoxHospitalitzacio> HosBoxHospitalitzacios { get; set; }

    public virtual DbSet<HosCompanyiaAsseguradora> HosCompanyiaAsseguradoras { get; set; }

    public virtual DbSet<HosConfiguracio> HosConfiguracios { get; set; }

    public virtual DbSet<HosDetallHospitalitzacio> HosDetallHospitalitzacios { get; set; }

    public virtual DbSet<HosDetallProva> HosDetallProvas { get; set; }

    public virtual DbSet<HosDetallTipusProva> HosDetallTipusProvas { get; set; }

    public virtual DbSet<HosDetallTipusTipusProva> HosDetallTipusTipusProvas { get; set; }

    public virtual DbSet<HosDoctor> HosDoctors { get; set; }

    public virtual DbSet<HosExpedientAsseguradora> HosExpedientAsseguradoras { get; set; }

    public virtual DbSet<HosFamArticleHospital> HosFamArticleHospitals { get; set; }

    public virtual DbSet<HosHospital> HosHospitals { get; set; }

    public virtual DbSet<HosHospitalitzacio> HosHospitalitzacios { get; set; }

    public virtual DbSet<HosMedicament> HosMedicaments { get; set; }

    public virtual DbSet<HosMedicamentReceptum> HosMedicamentRecepta { get; set; }

    public virtual DbSet<HosMotiuHospitalitzacio> HosMotiuHospitalitzacios { get; set; }

    public virtual DbSet<HosPacient> HosPacients { get; set; }

    public virtual DbSet<HosPacientConsultat> HosPacientConsultats { get; set; }

    public virtual DbSet<HosPatologiaPacient> HosPatologiaPacients { get; set; }

    public virtual DbSet<HosProgramaFillet> HosProgramaFillets { get; set; }

    public virtual DbSet<HosProva> HosProvas { get; set; }

    public virtual DbSet<HosReceptum> HosRecepta { get; set; }

    public virtual DbSet<HosResultatIdexx> HosResultatIdexxes { get; set; }

    public virtual DbSet<HosSalaEspera> HosSalaEsperas { get; set; }

    public virtual DbSet<HosTextVisitum> HosTextVisita { get; set; }

    public virtual DbSet<HosTipusBaixa> HosTipusBaixas { get; set; }

    public virtual DbSet<HosTipusProva> HosTipusProvas { get; set; }

    public virtual DbSet<HosTipusTipusProva> HosTipusTipusProvas { get; set; }

    public virtual DbSet<HosTipusVacuna> HosTipusVacunas { get; set; }

    public virtual DbSet<HosVacuna> HosVacunas { get; set; }

    public virtual DbSet<HosValorHospitalitzacio> HosValorHospitalitzacios { get; set; }

    public virtual DbSet<HosValoracioTipusProva> HosValoracioTipusProvas { get; set; }

    public virtual DbSet<HosVisitum> HosVisita { get; set; }

    public virtual DbSet<SlcArxiu> SlcArxius { get; set; }

    public virtual DbSet<SlcArxiuBinari> SlcArxiuBinaris { get; set; }

    public virtual DbSet<SlcBdConfig> SlcBdConfigs { get; set; }

    public virtual DbSet<SlcComunicat> SlcComunicats { get; set; }

    public virtual DbSet<SlcComunicatInforme> SlcComunicatInformes { get; set; }

    public virtual DbSet<SlcComunicatMail> SlcComunicatMails { get; set; }

    public virtual DbSet<SlcComunicatSm> SlcComunicatSms { get; set; }

    public virtual DbSet<SlcConfiguracio> SlcConfiguracios { get; set; }

    public virtual DbSet<SlcConnexio> SlcConnexios { get; set; }

    public virtual DbSet<SlcElementAlocat> SlcElementAlocats { get; set; }

    public virtual DbSet<SlcGrup> SlcGrups { get; set; }

    public virtual DbSet<SlcLlicencium> SlcLlicencia { get; set; }

    public virtual DbSet<SlcMacMaquina> SlcMacMaquinas { get; set; }

    public virtual DbSet<SlcMaquina> SlcMaquinas { get; set; }

    public virtual DbSet<SlcNotificacio> SlcNotificacios { get; set; }

    public virtual DbSet<SlcPersona> SlcPersonas { get; set; }

    public virtual DbSet<SlcPlantillaMail> SlcPlantillaMails { get; set; }

    public virtual DbSet<SlcPlantillaMailDefecte> SlcPlantillaMailDefectes { get; set; }

    public virtual DbSet<SlcPlantillaSm> SlcPlantillaSms { get; set; }

    public virtual DbSet<SlcPlantillaSmsDefecte> SlcPlantillaSmsDefectes { get; set; }

    public virtual DbSet<SlcPrivilegi> SlcPrivilegis { get; set; }

    public virtual DbSet<SlcRecordatori> SlcRecordatoris { get; set; }

    public virtual DbSet<SlcRelacionsExterne> SlcRelacionsExternes { get; set; }

    public virtual DbSet<SlcSobre> SlcSobres { get; set; }

    public virtual DbSet<SlcTelefon> SlcTelefons { get; set; }

    public virtual DbSet<SlcTextNotificacio> SlcTextNotificacios { get; set; }

    public virtual DbSet<SlcTipusNotificacio> SlcTipusNotificacios { get; set; }

    public virtual DbSet<SlcTipusNotificacioCartum> SlcTipusNotificacioCarta { get; set; }

    public virtual DbSet<SlcTipusNotificacioMail> SlcTipusNotificacioMails { get; set; }

    public virtual DbSet<SlcTipusNotificacioSm> SlcTipusNotificacioSms { get; set; }

    public virtual DbSet<SlcUsuari> SlcUsuaris { get; set; }

    public virtual DbSet<VetAnimal> VetAnimals { get; set; }

    public virtual DbSet<VetConfiguracio> VetConfiguracios { get; set; }

    public virtual DbSet<VetEspecie> VetEspecies { get; set; }

    public virtual DbSet<VetPesTaula> VetPesTaulas { get; set; }

    public virtual DbSet<VetPropietari> VetPropietaris { get; set; }

    public virtual DbSet<VetPropietariAntic> VetPropietariAntics { get; set; }

    public virtual DbSet<VetRasa> VetRasas { get; set; }

    public virtual DbSet<VetTaulaPe> VetTaulaPes { get; set; }

    public virtual DbSet<VetTextVisitum> VetTextVisita { get; set; }

    public virtual DbSet<VetVisitum> VetVisita { get; set; }

    public virtual DbSet<VetVistaDarrerPe> VetVistaDarrerPes { get; set; }

    public virtual DbSet<VetVistaDarreraAlsadum> VetVistaDarreraAlsada { get; set; }

    public virtual DbSet<VetVistaDarreraVisitum> VetVistaDarreraVisita { get; set; }

    public virtual DbSet<VetVistaDeute> VetVistaDeutes { get; set; }

    public virtual DbSet<VetVistaPerruquery> VetVistaPerruqueries { get; set; }

    public virtual DbSet<VistaArticlesActiu> VistaArticlesActius { get; set; }

    // OnConfiguring commentat per utilitzar la configuració de Program.cs
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("ConnectionString from appsettings.json");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgnAgendesVisible>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuari, e.IdAgenda });

            entity.ToTable("Agn_AgendesVisibles");

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.AgnAgendesVisibles)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK_Agn_AgendesVisibles_Agn_Agenda");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.AgnAgendesVisibles)
                .HasForeignKey(d => d.IdUsuari)
                .HasConstraintName("FK_Agn_AgendesVisibles_Slc_Usuari");
        });

        modelBuilder.Entity<AgnAgendum>(entity =>
        {
            entity.HasKey(e => e.IdAgenda);

            entity.ToTable("Agn_Agenda");

            entity.Property(e => e.IdAgenda).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.AgnAgenda)
                .HasForeignKey(d => d.IdColor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agn_Agenda_Agn_Agenda");
        });

        modelBuilder.Entity<AgnAvi>(entity =>
        {
            entity.HasKey(e => e.IdAvis);

            entity.ToTable("Agn_Avis");

            entity.Property(e => e.IdAvis).ValueGeneratedNever();
            entity.Property(e => e.Descripcio).HasMaxLength(50);
            entity.Property(e => e.DiaInici).HasColumnType("datetime");
            entity.Property(e => e.Frequencia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<AgnAvisAgendum>(entity =>
        {
            entity.HasKey(e => e.IdAvisAgenda).HasName("PK__Agn_Avis__6DC545A48C3FDC2F");

            entity.ToTable("Agn_AvisAgenda");

            entity.Property(e => e.IdAvisAgenda).ValueGeneratedNever();
            entity.Property(e => e.DiaAvis).HasColumnType("datetime");
            entity.Property(e => e.TextAvis).HasMaxLength(500);

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.AgnAvisAgenda)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK_Agn_AvisAgenda_Agn_Agenda");
        });

        modelBuilder.Entity<AgnCitum>(entity =>
        {
            entity.HasKey(e => e.IdCita);

            entity.ToTable("Agn_Cita");

            entity.Property(e => e.IdCita).ValueGeneratedNever();
            entity.Property(e => e.Descripcio).HasMaxLength(500);
            entity.Property(e => e.DiaCita).HasColumnType("datetime");
            entity.Property(e => e.DiaCreacio).HasColumnType("datetime");
            entity.Property(e => e.Duracio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.AgnCita)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK_Agn_Cita_Agn_Agenda");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.AgnCita)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK_Agn_Cita_Agn_Color");
        });

        modelBuilder.Entity<AgnColor>(entity =>
        {
            entity.HasKey(e => e.IdColor);

            entity.ToTable("Agn_Color");

            entity.Property(e => e.IdColor).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<AgnConfUsuari>(entity =>
        {
            entity.HasKey(e => e.IdUsuari).HasName("PK__Agn_ConfUsuari__45DE573A");

            entity.ToTable("Agn_ConfUsuari");

            entity.Property(e => e.IdUsuari).ValueGeneratedNever();
            entity.Property(e => e.FontSizeLinia1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FontSizeLinia2).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Zoom).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdUsuariNavigation).WithOne(p => p.AgnConfUsuari)
                .HasForeignKey<AgnConfUsuari>(d => d.IdUsuari)
                .HasConstraintName("FK_Agn_ConfUsuari_Slc_Usuari");
        });

        modelBuilder.Entity<AgnFestiu>(entity =>
        {
            entity.HasKey(e => e.IdFestiu).HasName("PK__Agn_Festiu__3E3D3572");

            entity.ToTable("Agn_Festiu");

            entity.Property(e => e.IdFestiu).ValueGeneratedNever();
            entity.Property(e => e.Descripcio).HasMaxLength(100);
            entity.Property(e => e.Dia).HasColumnType("datetime");

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.AgnFestius)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK_Agn_Festiu_Agn_Agenda");
        });

        modelBuilder.Entity<AgnHoraDisponible>(entity =>
        {
            entity.HasKey(e => e.IdHoraDisponible).HasName("PK__Agn_HoraDisponib__3C54ED00");

            entity.ToTable("Agn_HoraDisponible");

            entity.Property(e => e.IdHoraDisponible).ValueGeneratedNever();
            entity.Property(e => e.HoraFinal).HasColumnType("datetime");
            entity.Property(e => e.HoraInici).HasColumnType("datetime");

            entity.HasOne(d => d.IdAgendaNavigation).WithMany(p => p.AgnHoraDisponibles)
                .HasForeignKey(d => d.IdAgenda)
                .HasConstraintName("FK_Agn_HoraDisponible_Agn_Agenda");
        });

        modelBuilder.Entity<ArtArticle>(entity =>
        {
            entity.HasKey(e => e.IdArticle);

            entity.ToTable("Art_Article");

            entity.Property(e => e.IdArticle).ValueGeneratedNever();
            entity.Property(e => e.CodiBarres).HasMaxLength(50);
            entity.Property(e => e.CodiPropi).HasMaxLength(50);
            entity.Property(e => e.CodiProveidor).HasMaxLength(50);
            entity.Property(e => e.Estoc).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinimEstoc).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.NomVenda).HasMaxLength(200);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.ArtArticles)
                .HasForeignKey(d => d.IdFamilia)
                .HasConstraintName("FK_Art_Article_Art_Familia");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.ArtArticles)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Art_Article_Art_Marca");
        });

        modelBuilder.Entity<ArtCaixaArticle>(entity =>
        {
            entity.HasKey(e => e.IdCaixaArticle).HasName("PK__Art_Caix__8B60A63B5CC1BC92");

            entity.ToTable("Art_CaixaArticle");

            entity.Property(e => e.IdCaixaArticle).ValueGeneratedNever();
            entity.Property(e => e.DiaMoviment).HasColumnType("datetime");

            entity.HasOne(d => d.IdArticleArticleNavigation).WithMany(p => p.ArtCaixaArticleIdArticleArticleNavigations)
                .HasForeignKey(d => d.IdArticleArticle)
                .HasConstraintName("FK_Art_CaixaArticle_Art_ArticleArticle");

            entity.HasOne(d => d.IdArticleCaixaNavigation).WithMany(p => p.ArtCaixaArticleIdArticleCaixaNavigations)
                .HasForeignKey(d => d.IdArticleCaixa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_CaixaArticle_Art_ArticleCaixa");
        });

        modelBuilder.Entity<ArtConfiguracio>(entity =>
        {
            entity.HasKey(e => e.IdConfiguracio);

            entity.ToTable("Art_Configuracio");

            entity.Property(e => e.IdConfiguracio).ValueGeneratedNever();

            entity.HasOne(d => d.IdMagatzemNavigation).WithMany(p => p.ArtConfiguracios)
                .HasForeignKey(d => d.IdMagatzem)
                .HasConstraintName("FK_Art_Configuracio_Art_Magatzem");
        });

        modelBuilder.Entity<ArtFamilium>(entity =>
        {
            entity.HasKey(e => e.IdFamilia);

            entity.ToTable("Art_Familia");

            entity.Property(e => e.IdFamilia).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<ArtInventari>(entity =>
        {
            entity.HasKey(e => e.IdInventari);

            entity.ToTable("Art_Inventari");

            entity.Property(e => e.IdInventari).ValueGeneratedNever();
            entity.Property(e => e.DiaInventari).HasColumnType("datetime");
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<ArtMagatzem>(entity =>
        {
            entity.HasKey(e => e.IdMagatzem);

            entity.ToTable("Art_Magatzem");

            entity.Property(e => e.IdMagatzem).ValueGeneratedNever();

            entity.HasOne(d => d.IdMagatzemNavigation).WithOne(p => p.ArtMagatzem)
                .HasForeignKey<ArtMagatzem>(d => d.IdMagatzem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Magatzem_Slc_Persona");
        });

        modelBuilder.Entity<ArtMarca>(entity =>
        {
            entity.HasKey(e => e.IdMarca);

            entity.ToTable("Art_Marca");

            entity.Property(e => e.IdMarca).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<ArtMoviment>(entity =>
        {
            entity.HasKey(e => e.IdMoviment);

            entity.ToTable("Art_Moviment");

            entity.Property(e => e.IdMoviment).ValueGeneratedNever();
            entity.Property(e => e.DiaCaducitat).HasColumnType("datetime");
            entity.Property(e => e.DiaMoviment).HasColumnType("datetime");
            entity.Property(e => e.NumeroLot).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Unitats).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.ArtMoviments)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("FK_Art_Moviment_Art_Article");

            entity.HasOne(d => d.IdMagatzemNavigation).WithMany(p => p.ArtMoviments)
                .HasForeignKey(d => d.IdMagatzem)
                .HasConstraintName("FK_Art_Moviment_Art_Magatzem");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.ArtMoviments)
                .HasForeignKey(d => d.IdUsuari)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Moviment_Slc_Usuari");
        });

        modelBuilder.Entity<ArtMovimentInventari>(entity =>
        {
            entity.HasKey(e => new { e.IdInventari, e.IdMoviment }).HasName("PF_Art_MovimentInventari");

            entity.ToTable("Art_MovimentInventari");

            entity.HasOne(d => d.IdInventariNavigation).WithMany(p => p.ArtMovimentInventaris)
                .HasForeignKey(d => d.IdInventari)
                .HasConstraintName("FK_Art_MovimentInventari_Art_Inventari");

            entity.HasOne(d => d.IdMovimentNavigation).WithMany(p => p.ArtMovimentInventaris)
                .HasForeignKey(d => d.IdMoviment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_MovimentInventari_Art_Moviment");
        });

        modelBuilder.Entity<ArtRelArticleMagatzem>(entity =>
        {
            entity.HasKey(e => new { e.IdArticle, e.IdMagatzem });

            entity.ToTable("ArtRel_ArticleMagatzem");

            entity.Property(e => e.Estoc).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.ArtRelArticleMagatzems)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("FK_ArtRel_ArticleMagatzem_Art_Article");

            entity.HasOne(d => d.IdMagatzemNavigation).WithMany(p => p.ArtRelArticleMagatzems)
                .HasForeignKey(d => d.IdMagatzem)
                .HasConstraintName("FK_ArtRel_ArticleMagatzem_Art_Magatzem");
        });

        modelBuilder.Entity<ArtTraspa>(entity =>
        {
            entity.HasKey(e => e.IdTraspas);

            entity.ToTable("Art_Traspas");

            entity.Property(e => e.IdTraspas).ValueGeneratedNever();
            entity.Property(e => e.DiaTraspas).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Unitats).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.ArtTraspas)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Traspas_Art_Article");

            entity.HasOne(d => d.IdMagatzemDestiNavigation).WithMany(p => p.ArtTraspaIdMagatzemDestiNavigations)
                .HasForeignKey(d => d.IdMagatzemDesti)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Traspas_Art_Magatzem1");

            entity.HasOne(d => d.IdMagatzemOrigenNavigation).WithMany(p => p.ArtTraspaIdMagatzemOrigenNavigations)
                .HasForeignKey(d => d.IdMagatzemOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Art_Traspas_Art_Magatzem");
        });

        modelBuilder.Entity<FacAcompte>(entity =>
        {
            entity.HasKey(e => e.IdAcompte);

            entity.ToTable("Fac_Acompte");

            entity.Property(e => e.IdAcompte)
                .ValueGeneratedNever()
                .HasColumnName("IdACompte");
            entity.Property(e => e.DiaAcompte)
                .HasColumnType("datetime")
                .HasColumnName("DiaACompte");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Referencia).HasMaxLength(50);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacAcomptes)
                .HasForeignKey(d => d.IdCaixa)
                .HasConstraintName("FK_Fac_Acompte_Fac_Caixa");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacAcomptes)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Fac_Acompte_Fac_Client");

            entity.HasOne(d => d.IdReferenciaNavigation).WithMany(p => p.FacAcomptes)
                .HasForeignKey(d => d.IdReferencia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_Acompte_Vet_Animal");

            entity.HasOne(d => d.IdVenedorNavigation).WithMany(p => p.FacAcomptes)
                .HasForeignKey(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Acompte_Fac_Venedor");
        });

        modelBuilder.Entity<FacAlbara>(entity =>
        {
            entity.HasKey(e => e.IdAlbara);

            entity.ToTable("Fac_Albara");

            entity.Property(e => e.IdAlbara).ValueGeneratedNever();
            entity.Property(e => e.CodiAlbara).HasMaxLength(50);
            entity.Property(e => e.DiaAlbara).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.TotalAlbara).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.FacAlbaras)
                .HasForeignKey(d => d.IdComanda)
                .HasConstraintName("FK_Fac_Albara_Fac_Comanda");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacAlbaras)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Albara_Fac_Proveidor");
        });

        modelBuilder.Entity<FacAreaNegoci>(entity =>
        {
            entity.HasKey(e => e.IdAreaNegoci).HasName("PK__Fac_Area__61E5F9BF1503C2A0");

            entity.ToTable("Fac_AreaNegoci");

            entity.Property(e => e.IdAreaNegoci).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.FacAreaNegocis)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Empresa_Fac_AreaNegoci");
        });

        modelBuilder.Entity<FacArticle>(entity =>
        {
            entity.HasKey(e => e.IdArticle);

            entity.ToTable("Fac_Article");

            entity.Property(e => e.IdArticle).ValueGeneratedNever();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Preu).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdArticleNavigation).WithOne(p => p.FacArticle)
                .HasForeignKey<FacArticle>(d => d.IdArticle)
                .HasConstraintName("FK_Fac_Article_Art_Article");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.FacArticles)
                .HasForeignKey(d => d.IdFamilia)
                .HasConstraintName("FK_Fac_Article_Fac_Familia");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.FacArticles)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Article_Fac_Iva");
        });

        modelBuilder.Entity<FacArticleAlbara>(entity =>
        {
            entity.HasKey(e => e.IdArticleAlbara);

            entity.ToTable("Fac_ArticleAlbara");

            entity.Property(e => e.IdArticleAlbara).ValueGeneratedNever();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Descompte).HasColumnType("decimal(16, 4)");
            entity.Property(e => e.Unitats).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdAlbaraNavigation).WithMany(p => p.FacArticleAlbaras)
                .HasForeignKey(d => d.IdAlbara)
                .HasConstraintName("FK_Fac_ArticleAlbara_Fac_Albara");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacArticleAlbaras)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticleAlbara_Fac_Article");

            entity.HasOne(d => d.IdArticleComandaNavigation).WithMany(p => p.FacArticleAlbaras)
                .HasForeignKey(d => d.IdArticleComanda)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_ArticleAlbara_Fac_ArticleComanda");
        });

        modelBuilder.Entity<FacArticleCancelat>(entity =>
        {
            entity.HasKey(e => e.IdArticleCancelat);

            entity.ToTable("Fac_ArticleCancelat");

            entity.Property(e => e.IdArticleCancelat).ValueGeneratedNever();
            entity.Property(e => e.DiaCancelacio).HasColumnType("datetime");
            entity.Property(e => e.Motiu).HasMaxLength(500);
            entity.Property(e => e.Unitats).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdArticleComandaNavigation).WithMany(p => p.FacArticleCancelats)
                .HasForeignKey(d => d.IdArticleComanda)
                .HasConstraintName("FK_Fac_ArticleCancelat_Fac_ArticleComanda");
        });

        modelBuilder.Entity<FacArticleComandum>(entity =>
        {
            entity.HasKey(e => e.IdArticleComanda);

            entity.ToTable("Fac_ArticleComanda");

            entity.Property(e => e.IdArticleComanda).ValueGeneratedNever();
            entity.Property(e => e.DiaPerDemanar).HasColumnType("datetime");
            entity.Property(e => e.NomArticle).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Unitats).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacArticleComanda)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("FK_Fac_ArticleComanda_Fac_Article");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.FacArticleComanda)
                .HasForeignKey(d => d.IdComanda)
                .HasConstraintName("FK_Fac_ArticleComanda_Fac_Comanda");
        });

        modelBuilder.Entity<FacArticleFactura>(entity =>
        {
            entity.HasKey(e => e.IdArticleFactura);

            entity.ToTable("Fac_ArticleFactura");

            entity.Property(e => e.IdArticleFactura).ValueGeneratedNever();
            entity.Property(e => e.FactorIva).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FactorRecEquivalencia).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Impost).HasColumnType("numeric(18, 6)");
            entity.Property(e => e.NomIva).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.PreuNet).HasColumnType("numeric(18, 6)");
            entity.Property(e => e.Unitats).HasColumnType("numeric(18, 2)");

            entity.HasOne(d => d.IdArticleVenutNavigation).WithMany(p => p.FacArticleFacturas)
                .HasForeignKey(d => d.IdArticleVenut)
                .HasConstraintName("FK_Fac_ArticleFactura_Fac_ArticleVenut");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacArticleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Fac_ArticleFactura_Fac_Factura");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.FacArticleFacturas)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticleFactura_Fac_Iva");
        });

        modelBuilder.Entity<FacArticlePressupost>(entity =>
        {
            entity.HasKey(e => e.IdArticlePressupost).HasName("PK__Fac_ArticlePress__34B3CB38");

            entity.ToTable("Fac_ArticlePressupost");

            entity.Property(e => e.IdArticlePressupost).ValueGeneratedNever();
            entity.Property(e => e.Descompte).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.NomArticle).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Quantitat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ValorPreu).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacArticlePressuposts)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticlePressupost_Fac_Article");

            entity.HasOne(d => d.IdPressupostNavigation).WithMany(p => p.FacArticlePressuposts)
                .HasForeignKey(d => d.IdPressupost)
                .HasConstraintName("FK_Fac_ArticlePressupost_Fac_Pressupost");
        });

        modelBuilder.Entity<FacArticleReservat>(entity =>
        {
            entity.HasKey(e => e.IdArticleReservat).HasName("PK__Fac_Arti__A378266EA86DB72F");

            entity.ToTable("Fac_ArticleReservat");

            entity.Property(e => e.IdArticleReservat).ValueGeneratedNever();
            entity.Property(e => e.Cognom1).HasMaxLength(50);
            entity.Property(e => e.Cognom2).HasMaxLength(50);
            entity.Property(e => e.DiaRecepcio).HasColumnType("datetime");
            entity.Property(e => e.DiaReserva).HasColumnType("datetime");
            entity.Property(e => e.Mail).HasMaxLength(250);
            entity.Property(e => e.NomClient).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Telefon).HasMaxLength(20);

            entity.HasOne(d => d.IdArticleComandaNavigation).WithMany(p => p.FacArticleReservats)
                .HasForeignKey(d => d.IdArticleComanda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticleReservat_Fac_ArticleComanda");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacArticleReservats)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Fac_ArticleReservat_Fac_Client");

            entity.HasOne(d => d.IdUsuariRecepcioNavigation).WithMany(p => p.FacArticleReservatIdUsuariRecepcioNavigations)
                .HasForeignKey(d => d.IdUsuariRecepcio)
                .HasConstraintName("FK_Fac_ArticleReservat_Fac_VenedorRecepcio");

            entity.HasOne(d => d.IdUsuariReservaNavigation).WithMany(p => p.FacArticleReservatIdUsuariReservaNavigations)
                .HasForeignKey(d => d.IdUsuariReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticleReservat_Fac_VenedorReserva");
        });

        modelBuilder.Entity<FacArticleVenut>(entity =>
        {
            entity.HasKey(e => e.IdArticleVenut);

            entity.ToTable("Fac_ArticleVenut");

            entity.HasIndex(e => e.IdVenda, "IX_Fac_ArticleVenut");

            entity.Property(e => e.IdArticleVenut).ValueGeneratedNever();
            entity.Property(e => e.CostNet).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Descompte).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.DiaPagat).HasColumnType("datetime");
            entity.Property(e => e.FactorIva).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NomArticle).HasMaxLength(100);
            entity.Property(e => e.NomIva).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Quantitat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ValorPagat).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ValorPreu).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacArticleVenuts)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_ArticleVenut_Fac_Article");

            entity.HasOne(d => d.IdReglaTarifaNavigation).WithMany(p => p.FacArticleVenuts)
                .HasForeignKey(d => d.IdReglaTarifa)
                .HasConstraintName("FK_Fac_ArticleVenut_Fac_ReglaTarifa");

            entity.HasOne(d => d.IdVendaNavigation).WithMany(p => p.FacArticleVenuts)
                .HasForeignKey(d => d.IdVenda)
                .HasConstraintName("FK_Fac_ArticleVenut_Fac_Venda");
        });

        modelBuilder.Entity<FacCaixa>(entity =>
        {
            entity.HasKey(e => e.IdCaixa);

            entity.ToTable("Fac_Caixa");

            entity.Property(e => e.IdCaixa).ValueGeneratedNever();
            entity.Property(e => e.Codi).HasMaxLength(5);
            entity.Property(e => e.Comissio).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.NumeroCompte).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdEntitatBancariaNavigation).WithMany(p => p.FacCaixas)
                .HasForeignKey(d => d.IdEntitatBancaria)
                .HasConstraintName("FK_Fac_Caixa_Fac_EntitatBancaria");
        });

        modelBuilder.Entity<FacCentreCost>(entity =>
        {
            entity.HasKey(e => e.IdCentreCost).HasName("PK__Fac_Cent__392E86EE09802B2E");

            entity.ToTable("Fac_CentreCost");

            entity.Property(e => e.IdCentreCost).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<FacClient>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Fac_Client");

            entity.Property(e => e.IdClient).ValueGeneratedNever();
            entity.Property(e => e.AvisIntern).HasMaxLength(200);
            entity.Property(e => e.DadesPagament).HasMaxLength(250);
            entity.Property(e => e.Descompte).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroCompte).HasMaxLength(50);
            entity.Property(e => e.RetencioIrpf)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("RetencioIRPF");

            entity.HasOne(d => d.IdClientNavigation).WithOne(p => p.FacClient)
                .HasForeignKey<FacClient>(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Client_Slc_Persona");

            entity.HasOne(d => d.IdEntitatBancariaNavigation).WithMany(p => p.FacClients)
                .HasForeignKey(d => d.IdEntitatBancaria)
                .HasConstraintName("FK_Fac_Client_Fac_EntitatBancaria");

            entity.HasOne(d => d.IdTipusTarifaNavigation).WithMany(p => p.FacClients)
                .HasForeignKey(d => d.IdTipusTarifa)
                .HasConstraintName("FK_Fac_Client_Fac_TipusTarifa");
        });

        modelBuilder.Entity<FacComandum>(entity =>
        {
            entity.HasKey(e => e.IdComanda);

            entity.ToTable("Fac_Comanda");

            entity.Property(e => e.IdComanda).ValueGeneratedNever();
            entity.Property(e => e.DiaComanda).HasColumnType("datetime");
            entity.Property(e => e.DiaInici).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Operador).HasMaxLength(50);

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacComanda)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Comanda_Fac_Proveidor");
        });

        modelBuilder.Entity<FacCompteBanc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Fac_CompteBanc");

            entity.Property(e => e.CodiCompte).HasMaxLength(30);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdClientNavigation).WithMany()
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_CompteBanc_Fac_Client");
        });

        modelBuilder.Entity<FacConfiguracio>(entity =>
        {
            entity.HasKey(e => e.IdConfiguracio);

            entity.ToTable("Fac_Configuracio");

            entity.Property(e => e.IdConfiguracio).ValueGeneratedNever();
            entity.Property(e => e.ImprimirLpd).HasColumnName("ImprimirLPD");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.FacConfiguracios)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Configuracio_Fac_Empresa");

            entity.HasOne(d => d.IdMostradorNavigation).WithMany(p => p.FacConfiguracios)
                .HasForeignKey(d => d.IdMostrador)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_Configuracio_Fac_Client");
        });

        modelBuilder.Entity<FacDespesa>(entity =>
        {
            entity.HasKey(e => e.IdDespesa);

            entity.ToTable("Fac_Despesa");

            entity.Property(e => e.IdDespesa).ValueGeneratedNever();
            entity.Property(e => e.DiaDespesa).HasColumnType("datetime");
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacDespesas)
                .HasForeignKey(d => d.IdCaixa)
                .HasConstraintName("FK_Fac_Despesa_Fac_Caixa");
        });

        modelBuilder.Entity<FacEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);

            entity.ToTable("Fac_Empresa");

            entity.Property(e => e.IdEmpresa).ValueGeneratedNever();
            entity.Property(e => e.CodiEmpresa).HasMaxLength(2);

            entity.HasOne(d => d.IdCentreCostNavigation).WithMany(p => p.FacEmpresas)
                .HasForeignKey(d => d.IdCentreCost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Empresa_Fac_CentreCost");

            entity.HasOne(d => d.IdEmpresaNavigation).WithOne(p => p.FacEmpresa)
                .HasForeignKey<FacEmpresa>(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Empresa_Slc_Persona");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.FacEmpresas)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_Empresa_Fac_Iva");
        });

        modelBuilder.Entity<FacEntitatBancarium>(entity =>
        {
            entity.HasKey(e => e.IdEntitatBancaria);

            entity.ToTable("Fac_EntitatBancaria");

            entity.Property(e => e.IdEntitatBancaria).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<FacFactura>(entity =>
        {
            entity.HasKey(e => e.IdFactura);

            entity.ToTable("Fac_Factura");

            entity.Property(e => e.IdFactura).ValueGeneratedNever();
            entity.Property(e => e.AdresaEmpresa).HasMaxLength(200);
            entity.Property(e => e.CodiFactura).HasMaxLength(50);
            entity.Property(e => e.DadesEmpresa).HasMaxLength(500);
            entity.Property(e => e.DiaFactura).HasColumnType("datetime");
            entity.Property(e => e.DiaPagada).HasColumnType("datetime");
            entity.Property(e => e.DiaVenciment).HasColumnType("datetime");
            entity.Property(e => e.EmailEmpresa).HasMaxLength(200);
            entity.Property(e => e.FactorIrpf)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("FactorIRPF");
            entity.Property(e => e.NifEmpresa).HasMaxLength(15);
            entity.Property(e => e.NomEmpresa).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.PoblacioEmpresa).HasMaxLength(200);
            entity.Property(e => e.TelefonsEmpresa).HasMaxLength(100);
            entity.Property(e => e.TotalFactura).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalNet).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.ValorIrpf)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ValorIRPF");

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacFacturas)
                .HasForeignKey(d => d.IdCaixa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Factura_Fac_Caixa");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.FacFacturas)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Factura_Fac_Empresa");

            entity.HasOne(d => d.IdPeriodicaNavigation).WithMany(p => p.FacFacturas)
                .HasForeignKey(d => d.IdPeriodica)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_Factura_Fac_Periodica");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.FacFacturas)
                .HasForeignKey(d => d.IdUsuari)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Factura_Slc_Usuari");
        });

        modelBuilder.Entity<FacFacturaClient>(entity =>
        {
            entity.HasKey(e => new { e.IdFactura, e.IdClient });

            entity.ToTable("Fac_FacturaClient");

            entity.Property(e => e.AdresaClient).HasMaxLength(200);
            entity.Property(e => e.NifClient).HasMaxLength(20);
            entity.Property(e => e.NomClient).HasMaxLength(200);
            entity.Property(e => e.PoblacioClient).HasMaxLength(200);
            entity.Property(e => e.RetencioIrpf)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("RetencioIRPF");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacFacturaClients)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_FacturaClient_Fac_Client");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacFacturaClients)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Fac_FacturaClient_Fac_Factura");
        });

        modelBuilder.Entity<FacFacturaProveidor>(entity =>
        {
            entity.HasKey(e => e.IdFactura);

            entity.ToTable("Fac_FacturaProveidor");

            entity.Property(e => e.IdFactura).ValueGeneratedNever();
            entity.Property(e => e.Consum).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Descompte).HasColumnType("decimal(16, 4)");
            entity.Property(e => e.FinPeriode).HasColumnType("datetime");
            entity.Property(e => e.IniPeriode).HasColumnType("datetime");

            entity.HasOne(d => d.IdFacturaNavigation).WithOne(p => p.FacFacturaProveidor)
                .HasForeignKey<FacFacturaProveidor>(d => d.IdFactura)
                .HasConstraintName("FK_Fac_FacturaProveidor_Fac_Factura");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacFacturaProveidors)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_FacturaProveidor_Fac_Proveidor");
        });

        modelBuilder.Entity<FacFamilium>(entity =>
        {
            entity.HasKey(e => e.IdFamilia);

            entity.ToTable("Fac_Familia");

            entity.Property(e => e.IdFamilia).ValueGeneratedNever();

            entity.HasOne(d => d.IdCentreCostNavigation).WithMany(p => p.FacFamilia)
                .HasForeignKey(d => d.IdCentreCost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Familia_Fac_CentreCost");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.FacFamilia)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Fac_Familia_Fac_Empresa");

            entity.HasOne(d => d.IdFamiliaNavigation).WithOne(p => p.FacFamilium)
                .HasForeignKey<FacFamilium>(d => d.IdFamilia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Familia_Art_Familia");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.FacFamilia)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Familia_Fac_Iva");
        });

        modelBuilder.Entity<FacIva>(entity =>
        {
            entity.HasKey(e => e.IdIva).HasName("PK_Vet_Iva");

            entity.ToTable("Fac_Iva");

            entity.Property(e => e.IdIva).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.RecEquiv).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FacIvaFactura>(entity =>
        {
            entity.HasKey(e => new { e.IdFactura, e.IdIva });

            entity.ToTable("Fac_IvaFactura");

            entity.Property(e => e.FactorIva).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.RecEquivalencia).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalIva).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.TotalNet).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacIvaFacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Fac_IvaFactura_Fac_Factura");

            entity.HasOne(d => d.IdIvaNavigation).WithMany(p => p.FacIvaFacturas)
                .HasForeignKey(d => d.IdIva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_IvaFactura_Fac_Iva");
        });

        modelBuilder.Entity<FacNotificacioClient>(entity =>
        {
            entity.HasKey(e => new { e.IdNotificacio, e.IdClient });

            entity.ToTable("Fac_NotificacioClient");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacNotificacioClients)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Fac_NotificacioClient_Fac_Client");
        });

        modelBuilder.Entity<FacPerfilTarifa>(entity =>
        {
            entity.HasKey(e => e.IdPerfilTarifa).HasName("PK__Fac_Perf__9E856CBC53B73F64");

            entity.ToTable("Fac_PerfilTarifa");

            entity.Property(e => e.IdPerfilTarifa).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasMany(d => d.IdClients).WithMany(p => p.IdPerfilTarifas)
                .UsingEntity<Dictionary<string, object>>(
                    "FacRelPerfilTarifaClient",
                    r => r.HasOne<FacClient>().WithMany()
                        .HasForeignKey("IdClient")
                        .HasConstraintName("FK_FacRel_PerfilTarifa_Client_Fac_Client"),
                    l => l.HasOne<FacPerfilTarifa>().WithMany()
                        .HasForeignKey("IdPerfilTarifa")
                        .HasConstraintName("FK_FacRel_PerfilTarifa_Client_Fac_PerfilTarifa"),
                    j =>
                    {
                        j.HasKey("IdPerfilTarifa", "IdClient");
                        j.ToTable("FacRel_PerfilTarifa_Client");
                    });
        });

        modelBuilder.Entity<FacPeriodica>(entity =>
        {
            entity.HasKey(e => e.IdPeriodica);

            entity.ToTable("Fac_Periodica");

            entity.Property(e => e.IdPeriodica).ValueGeneratedNever();
            entity.Property(e => e.DiaInici).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacPeriodicas)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Periodica_Fac_Proveidor");
        });

        modelBuilder.Entity<FacPressupost>(entity =>
        {
            entity.HasKey(e => e.IdPressupost).HasName("PK__Fac_Pressupost__31D75E8D");

            entity.ToTable("Fac_Pressupost");

            entity.Property(e => e.IdPressupost).ValueGeneratedNever();
            entity.Property(e => e.Caducitat).HasColumnType("datetime");
            entity.Property(e => e.DiaPressupost).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacPressuposts)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Fac_Pressupost_Fac_Client");

            entity.HasOne(d => d.IdVenedorNavigation).WithMany(p => p.FacPressuposts)
                .HasForeignKey(d => d.IdVenedor)
                .HasConstraintName("FK_Fac_Pressupost_Fac_Venedor");
        });

        modelBuilder.Entity<FacProveidor>(entity =>
        {
            entity.HasKey(e => e.IdProveidor);

            entity.ToTable("Fac_Proveidor");

            entity.Property(e => e.IdProveidor).ValueGeneratedNever();
            entity.Property(e => e.Descompte).HasColumnType("decimal(16, 4)");
            entity.Property(e => e.NomFiscal).HasMaxLength(200);

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacProveidors)
                .HasForeignKey(d => d.IdCaixa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Proveidor_Fac_Caixa");

            entity.HasOne(d => d.IdProveidorNavigation).WithOne(p => p.FacProveidor)
                .HasForeignKey<FacProveidor>(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Proveidor_Slc_Persona");
        });

        modelBuilder.Entity<FacReglaTarifa>(entity =>
        {
            entity.HasKey(e => e.IdReglaTarifa).HasName("PK__Fac_Regl__B1FD69987DBF1F36");

            entity.ToTable("Fac_ReglaTarifa");

            entity.Property(e => e.IdReglaTarifa).ValueGeneratedNever();
            entity.Property(e => e.DiaFinal).HasColumnType("datetime");
            entity.Property(e => e.DiaInici).HasColumnType("datetime");
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdPerfilTarifaNavigation).WithMany(p => p.FacReglaTarifas)
                .HasForeignKey(d => d.IdPerfilTarifa)
                .HasConstraintName("FK_Fac_ReglaTarifa_PerfilTarifa");
        });

        modelBuilder.Entity<FacRelAreaNegociCentreCost>(entity =>
        {
            entity.HasKey(e => new { e.IdAreaNegoci, e.IdCentreCost });

            entity.ToTable("FacRel_AreaNegoci_CentreCost");

            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Percentantge).HasColumnType("decimal(16, 4)");

            entity.HasOne(d => d.IdAreaNegociNavigation).WithMany(p => p.FacRelAreaNegociCentreCosts)
                .HasForeignKey(d => d.IdAreaNegoci)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacRel_AreaNegoci_CentreCost_Fac_AreaNegoci");

            entity.HasOne(d => d.IdCentreCostNavigation).WithMany(p => p.FacRelAreaNegociCentreCosts)
                .HasForeignKey(d => d.IdCentreCost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacRel_AreaNegoci_CentreCost_Fac_CentreCost");
        });

        modelBuilder.Entity<FacRelArticleProveidor>(entity =>
        {
            entity.HasKey(e => new { e.IdArticle, e.IdProveidor });

            entity.ToTable("FacRel_ArticleProveidor");

            entity.Property(e => e.Codi).HasMaxLength(100);

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacRelArticleProveidors)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacRel_ArticleProveidor_Fac_Article");

            entity.HasOne(d => d.IdProveidorNavigation).WithMany(p => p.FacRelArticleProveidors)
                .HasForeignKey(d => d.IdProveidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacRel_ArticleProveidor_Fac_Proveidor");
        });

        modelBuilder.Entity<FacRelPerfilTarifaFamilium>(entity =>
        {
            entity.HasKey(e => new { e.IdPerfilTarifa, e.IdFamilia });

            entity.ToTable("FacRel_PerfilTarifa_Familia");

            entity.Property(e => e.Descompte).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.FacRelPerfilTarifaFamilia)
                .HasForeignKey(d => d.IdFamilia)
                .HasConstraintName("FK_FacRel_PerfilTarifa_Familia_Fac_Familia");

            entity.HasOne(d => d.IdPerfilTarifaNavigation).WithMany(p => p.FacRelPerfilTarifaFamilia)
                .HasForeignKey(d => d.IdPerfilTarifa)
                .HasConstraintName("FK_FacRel_PerfilTarifa_Familia_PerfilTarifa");
        });

        modelBuilder.Entity<FacRelReglaTarifaArticle>(entity =>
        {
            entity.HasKey(e => new { e.IdReglaTarifa, e.IdArticle });

            entity.ToTable("FacRel_ReglaTarifa_Article");

            entity.Property(e => e.Descompte).HasColumnType("decimal(15, 6)");
            entity.Property(e => e.Preu).HasColumnType("decimal(15, 6)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacRelReglaTarifaArticles)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("FK_FacRel_ReglaTarifa_Article_Fac_Article");

            entity.HasOne(d => d.IdReglaTarifaNavigation).WithMany(p => p.FacRelReglaTarifaArticles)
                .HasForeignKey(d => d.IdReglaTarifa)
                .HasConstraintName("FK_FacRel_ReglaTarifa_Article_Fac_PerfilTarifa");
        });

        modelBuilder.Entity<FacRelTarifaFamilium>(entity =>
        {
            entity.HasKey(e => new { e.IdFamilia, e.IdClient });

            entity.ToTable("FacRel_TarifaFamilia");

            entity.Property(e => e.Descompte).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacRelTarifaFamilia)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_FacRel_TarifaFamilia_Fac_Client");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.FacRelTarifaFamilia)
                .HasForeignKey(d => d.IdFamilia)
                .HasConstraintName("FK_FacRel_TarifaFamilia_Fac_Familia");

            entity.HasOne(d => d.IdTipusTarifaNavigation).WithMany(p => p.FacRelTarifaFamilia)
                .HasForeignKey(d => d.IdTipusTarifa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FacRel_TarifaFamilia_Fac_TipusTarifa");
        });

        modelBuilder.Entity<FacTancament>(entity =>
        {
            entity.HasKey(e => e.IdTancament);

            entity.ToTable("Fac_Tancament");

            entity.HasIndex(e => e.IdCaixa, "IX_Fac_Tancament");

            entity.Property(e => e.IdTancament).ValueGeneratedNever();
            entity.Property(e => e.Dia).HasColumnType("datetime");
            entity.Property(e => e.MotiuDesquadre).HasMaxLength(100);
            entity.Property(e => e.ValorDesquadre).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorFinal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacTancaments)
                .HasForeignKey(d => d.IdCaixa)
                .HasConstraintName("FK_Fac_Tancament_Fac_Caixa");

            entity.HasOne(d => d.IdVenedorNavigation).WithMany(p => p.FacTancaments)
                .HasForeignKey(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Tancament_Fac_Venedor");
        });

        modelBuilder.Entity<FacTarifa>(entity =>
        {
            entity.HasKey(e => new { e.IdArticle, e.IdTipusTarifa });

            entity.ToTable("Fac_Tarifa");

            entity.Property(e => e.Preu).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.FacTarifas)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("FK_Fac_Tarifa_Fac_Article");

            entity.HasOne(d => d.IdTipusTarifaNavigation).WithMany(p => p.FacTarifas)
                .HasForeignKey(d => d.IdTipusTarifa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Tarifa_Fac_TipusTarifa");
        });

        modelBuilder.Entity<FacTermini>(entity =>
        {
            entity.HasKey(e => e.IdTermini);

            entity.ToTable("Fac_Termini");

            entity.Property(e => e.IdTermini).ValueGeneratedNever();
            entity.Property(e => e.DiaPagat).HasColumnType("datetime");
            entity.Property(e => e.DiaTermini).HasColumnType("datetime");
            entity.Property(e => e.DiaVenciment).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.TotalNet).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTermini).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacTerminis)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Fac_Termini_Fac_Factura");
        });

        modelBuilder.Entity<FacTipusTarifa>(entity =>
        {
            entity.HasKey(e => e.IdTipusTarifa);

            entity.ToTable("Fac_TipusTarifa");

            entity.Property(e => e.IdTipusTarifa).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<FacTraspa>(entity =>
        {
            entity.HasKey(e => e.IdTraspas);

            entity.ToTable("Fac_Traspas");

            entity.Property(e => e.IdTraspas).ValueGeneratedNever();
            entity.Property(e => e.DiaTraspas).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCaixaDestiNavigation).WithMany(p => p.FacTraspaIdCaixaDestiNavigations)
                .HasForeignKey(d => d.IdCaixaDesti)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Traspas_Fac_Caixa1");

            entity.HasOne(d => d.IdCaixaOrigenNavigation).WithMany(p => p.FacTraspaIdCaixaOrigenNavigations)
                .HasForeignKey(d => d.IdCaixaOrigen)
                .HasConstraintName("FK_Fac_Traspas_Fac_Caixa");

            entity.HasOne(d => d.IdVenedorNavigation).WithMany(p => p.FacTraspas)
                .HasForeignKey(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Traspas_Fac_Venedor");
        });

        modelBuilder.Entity<FacVendum>(entity =>
        {
            entity.HasKey(e => e.IdVenda);

            entity.ToTable("Fac_Venda");

            entity.HasIndex(e => new { e.DiaVenda, e.IdClient }, "IX_Fac_Venda");

            entity.Property(e => e.IdVenda).ValueGeneratedNever();
            entity.Property(e => e.ComunicarFacturaDiaExecucio)
                .HasColumnType("datetime")
                .HasColumnName("ComunicarFactura_DiaExecucio");
            entity.Property(e => e.ComunicarFacturaDiaPeticio)
                .HasColumnType("datetime")
                .HasColumnName("ComunicarFactura_DiaPeticio");
            entity.Property(e => e.ComunicarFacturaMail)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ComunicarFactura_Mail");
            entity.Property(e => e.ComunicarFacturaUsusari)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ComunicarFactura_Ususari");
            entity.Property(e => e.DiaPagat).HasColumnType("datetime");
            entity.Property(e => e.DiaVenda).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Referencia).HasMaxLength(50);
            entity.Property(e => e.Resum).HasMaxLength(500);
            entity.Property(e => e.TotalCanvi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPagat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalVenda).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCaixaNavigation).WithMany(p => p.FacVenda)
                .HasForeignKey(d => d.IdCaixa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Venda_Fac_Caixa");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.FacVenda)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Fac_Venda_Fac_Client");

            entity.HasOne(d => d.IdReferenciaNavigation).WithMany(p => p.FacVenda)
                .HasForeignKey(d => d.IdReferencia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Fac_Venda_Vet_Animal");

            entity.HasOne(d => d.IdVenedorNavigation).WithMany(p => p.FacVenda)
                .HasForeignKey(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Venda_Fac_Venedor");
        });

        modelBuilder.Entity<FacVenedor>(entity =>
        {
            entity.HasKey(e => e.IdVenedor);

            entity.ToTable("Fac_Venedor");

            entity.Property(e => e.IdVenedor).ValueGeneratedNever();

            entity.HasOne(d => d.IdVenedorNavigation).WithOne(p => p.FacVenedor)
                .HasForeignKey<FacVenedor>(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Venedor_Slc_Persona");

            entity.HasOne(d => d.IdVenedor1).WithOne(p => p.FacVenedor)
                .HasForeignKey<FacVenedor>(d => d.IdVenedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fac_Venedor_Slc_Usuari");
        });

        modelBuilder.Entity<HosArticleHospital>(entity =>
        {
            entity.HasKey(e => new { e.IdArticleHospital, e.Tipus }).HasName("PF_Hos_ArticleHospital");

            entity.ToTable("Hos_ArticleHospital");

            entity.HasOne(d => d.IdArticleHospitalNavigation).WithMany(p => p.HosArticleHospitals)
                .HasForeignKey(d => d.IdArticleHospital)
                .HasConstraintName("PF_Hos_ArticleHospital_Fac_Article");
        });

        modelBuilder.Entity<HosArxiuVisitum>(entity =>
        {
            entity.HasKey(e => new { e.IdVisita, e.IdArxiu });

            entity.ToTable("Hos_ArxiuVisita");

            entity.HasOne(d => d.IdArxiuNavigation).WithMany(p => p.HosArxiuVisita)
                .HasForeignKey(d => d.IdArxiu)
                .HasConstraintName("FK_Hos_ArxiuVisita_Slc_Arxiu");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.HosArxiuVisita)
                .HasForeignKey(d => d.IdVisita)
                .HasConstraintName("FK_Hos_ArxiuVisita_Hos_Visita");
        });

        modelBuilder.Entity<HosBaixa>(entity =>
        {
            entity.HasKey(e => e.IdPacient).HasName("PK__Hos_Baixa__08D548FA");

            entity.ToTable("Hos_Baixa");

            entity.Property(e => e.IdPacient).ValueGeneratedNever();
            entity.Property(e => e.DiaBaixa).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdPacientNavigation).WithOne(p => p.HosBaixa)
                .HasForeignKey<HosBaixa>(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Baixa_Hos_Pacient");

            entity.HasOne(d => d.IdTipusBaixaNavigation).WithMany(p => p.HosBaixas)
                .HasForeignKey(d => d.IdTipusBaixa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Baixa_Hos_TipusBaixa");
        });

        modelBuilder.Entity<HosBoxHospitalitzacio>(entity =>
        {
            entity.HasKey(e => e.IdBoxHospitalitzacio).HasName("PK__Hos_BoxH__AC485D10C08C4D5D");

            entity.ToTable("Hos_BoxHospitalitzacio");

            entity.Property(e => e.IdBoxHospitalitzacio).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HosCompanyiaAsseguradora>(entity =>
        {
            entity.HasKey(e => e.IdCompanyiaAsseguradora).HasName("PK__Hos_Comp__2F48F7076D16E10B");

            entity.ToTable("Hos_CompanyiaAsseguradora");

            entity.Property(e => e.IdCompanyiaAsseguradora).ValueGeneratedNever();

            entity.HasOne(d => d.IdCompanyiaAsseguradoraNavigation).WithOne(p => p.HosCompanyiaAsseguradora)
                .HasForeignKey<HosCompanyiaAsseguradora>(d => d.IdCompanyiaAsseguradora)
                .HasConstraintName("FK_Hos_CompanyiaAsseguradora_Slc_Pacient");
        });

        modelBuilder.Entity<HosConfiguracio>(entity =>
        {
            entity.HasKey(e => e.IdConfiguracio).HasName("PK__Hos_Configuracio__74CE504D");

            entity.ToTable("Hos_Configuracio");

            entity.Property(e => e.IdConfiguracio).ValueGeneratedNever();
            entity.Property(e => e.GenId).HasColumnName("GenID");

            entity.HasOne(d => d.IdHospitalNavigation).WithMany(p => p.HosConfiguracios)
                .HasForeignKey(d => d.IdHospital)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Configuracio_Hos_Hospital");

            entity.HasOne(d => d.IdTarifaHospitalitzacioNavigation).WithMany(p => p.HosConfiguracios)
                .HasForeignKey(d => d.IdTarifaHospitalitzacio)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_Configuracio_Fac_TipusTarifa");
        });

        modelBuilder.Entity<HosDetallHospitalitzacio>(entity =>
        {
            entity.HasKey(e => e.IdDetallHospitalitzacio).HasName("PK__Hos_Deta__AE94B697CD787EAF");

            entity.ToTable("Hos_DetallHospitalitzacio");

            entity.Property(e => e.IdDetallHospitalitzacio).ValueGeneratedNever();
            entity.Property(e => e.HoraInici).HasColumnType("datetime");
            entity.Property(e => e.Nom)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Observacions)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.HosDetallHospitalitzacios)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_DetallHospitalitzacio_Fac_Article");

            entity.HasOne(d => d.IdHospitalitzacioNavigation).WithMany(p => p.HosDetallHospitalitzacios)
                .HasForeignKey(d => d.IdHospitalitzacio)
                .HasConstraintName("FK_Hos_DetallHospitalitzacio_Hos_Hospitalitzacio");
        });

        modelBuilder.Entity<HosDetallProva>(entity =>
        {
            entity.HasKey(e => e.IdDetallProva).HasName("PK__Hos_DetallProva__18178C8A");

            entity.ToTable("Hos_DetallProva");

            entity.Property(e => e.IdDetallProva).ValueGeneratedNever();
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Valor).HasMaxLength(100);

            entity.HasOne(d => d.IdDetallTipusProvaNavigation).WithMany(p => p.HosDetallProvas)
                .HasForeignKey(d => d.IdDetallTipusProva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_DetallProva_Hos_DetallTipusProva");

            entity.HasOne(d => d.IdProvaNavigation).WithMany(p => p.HosDetallProvas)
                .HasForeignKey(d => d.IdProva)
                .HasConstraintName("FK_Hos_DetallProva_Hos_Prova");
        });

        modelBuilder.Entity<HosDetallTipusProva>(entity =>
        {
            entity.HasKey(e => e.IdDetallTipusProva).HasName("PK__Hos_DetallTipusP__0E8E2250");

            entity.ToTable("Hos_DetallTipusProva");

            entity.Property(e => e.IdDetallTipusProva).ValueGeneratedNever();
            entity.Property(e => e.Formula).HasMaxLength(200);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Unitats).HasMaxLength(50);

            entity.HasOne(d => d.IdTipusProvaNavigation).WithMany(p => p.HosDetallTipusProvas)
                .HasForeignKey(d => d.IdTipusProva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_DetallTipusProva_Hos_TipusProva");
        });

        modelBuilder.Entity<HosDetallTipusTipusProva>(entity =>
        {
            entity.HasKey(e => new { e.IdDetallTipusProva, e.Tipus });

            entity.ToTable("Hos_DetallTipusTipusProva");

            entity.Property(e => e.NomValor).HasMaxLength(50);

            entity.HasOne(d => d.IdDetallTipusProvaNavigation).WithMany(p => p.HosDetallTipusTipusProvas)
                .HasForeignKey(d => d.IdDetallTipusProva)
                .HasConstraintName("FK_Hos_DetallTipusTipusProva_Hos_TipusProva");
        });

        modelBuilder.Entity<HosDoctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__Hos_Doctor__6F1576F7");

            entity.ToTable("Hos_Doctor");

            entity.Property(e => e.IdDoctor).ValueGeneratedNever();
            entity.Property(e => e.Especialitat).HasMaxLength(50);
            entity.Property(e => e.NumColegiat).HasMaxLength(50);

            entity.HasOne(d => d.IdDoctorNavigation).WithOne(p => p.HosDoctor)
                .HasForeignKey<HosDoctor>(d => d.IdDoctor)
                .HasConstraintName("FK_Hos_Doctor_Slc_Persona");

            entity.HasOne(d => d.IdDoctor1).WithOne(p => p.HosDoctor)
                .HasForeignKey<HosDoctor>(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Doctor_Slc_Usuari");

            entity.HasOne(d => d.IdHospitalNavigation).WithMany(p => p.HosDoctors)
                .HasForeignKey(d => d.IdHospital)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Doctor_Hos_Hospital");
        });

        modelBuilder.Entity<HosExpedientAsseguradora>(entity =>
        {
            entity.HasKey(e => e.IdExpedientAsseguradora).HasName("PK__Hos_Expe__F4489FE6AA722280");

            entity.ToTable("Hos_ExpedientAsseguradora");

            entity.Property(e => e.IdExpedientAsseguradora).ValueGeneratedNever();
            entity.Property(e => e.Actiu).HasDefaultValue(true);
            entity.Property(e => e.CodiExpedient).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdCompanyiaAsseguradoraNavigation).WithMany(p => p.HosExpedientAsseguradoras)
                .HasForeignKey(d => d.IdCompanyiaAsseguradora)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_ExpedientAsseguradora_Hos_CompanyiaAsseguradora");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosExpedientAsseguradoras)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_ExpedientAsseguradora_Hos_Pacient");
        });

        modelBuilder.Entity<HosFamArticleHospital>(entity =>
        {
            entity.HasKey(e => new { e.IdFamArticleHospital, e.Tipus });

            entity.ToTable("Hos_FamArticleHospital");

            entity.HasOne(d => d.IdFamArticleHospitalNavigation).WithMany(p => p.HosFamArticleHospitals)
                .HasForeignKey(d => d.IdFamArticleHospital)
                .HasConstraintName("FK_Hos_FamArticleHospital_Fac_Familia");
        });

        modelBuilder.Entity<HosHospital>(entity =>
        {
            entity.HasKey(e => e.IdHospital).HasName("PK__Hos_Hospital__6C390A4C");

            entity.ToTable("Hos_Hospital");

            entity.Property(e => e.IdHospital).ValueGeneratedNever();

            entity.HasOne(d => d.IdHospitalNavigation).WithOne(p => p.HosHospital)
                .HasForeignKey<HosHospital>(d => d.IdHospital)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Hospital_Fac_Empresa");

            entity.HasOne(d => d.IdHospital1).WithOne(p => p.HosHospital)
                .HasForeignKey<HosHospital>(d => d.IdHospital)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Hospital_Slc_Persona");
        });

        modelBuilder.Entity<HosHospitalitzacio>(entity =>
        {
            entity.HasKey(e => e.IdHospitalitzacio).HasName("PK__Hos_Hosp__077AA3A00BBC50D6");

            entity.ToTable("Hos_Hospitalitzacio");

            entity.Property(e => e.IdHospitalitzacio).ValueGeneratedNever();
            entity.Property(e => e.DiaAlta).HasColumnType("datetime");
            entity.Property(e => e.DiaIngres).HasColumnType("datetime");
            entity.Property(e => e.ObservacionsAlta)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ObservacionsIngres)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBoxHospitalitzacioNavigation).WithMany(p => p.HosHospitalitzacios)
                .HasForeignKey(d => d.IdBoxHospitalitzacio)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_BoxHospitalitzacio");

            entity.HasOne(d => d.IdDoctorAltaNavigation).WithMany(p => p.HosHospitalitzacioIdDoctorAltaNavigations)
                .HasForeignKey(d => d.IdDoctorAlta)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_DoctorAlta");

            entity.HasOne(d => d.IdDoctorIngresNavigation).WithMany(p => p.HosHospitalitzacioIdDoctorIngresNavigations)
                .HasForeignKey(d => d.IdDoctorIngres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_DoctorIngres");

            entity.HasOne(d => d.IdMotiuAltaNavigation).WithMany(p => p.HosHospitalitzacioIdMotiuAltaNavigations)
                .HasForeignKey(d => d.IdMotiuAlta)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_MotiuAlta");

            entity.HasOne(d => d.IdMotiuIngresNavigation).WithMany(p => p.HosHospitalitzacioIdMotiuIngresNavigations)
                .HasForeignKey(d => d.IdMotiuIngres)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_MotiuIngres");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosHospitalitzacios)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Hospitalitzacio_Hos_Pacient");
        });

        modelBuilder.Entity<HosMedicament>(entity =>
        {
            entity.HasKey(e => e.IdMedicament).HasName("PK__Hos_Medicament__257187A8");

            entity.ToTable("Hos_Medicament");

            entity.HasIndex(e => e.CimavetNregistro, "idx_medicament_cimavet_nregistro");

            entity.Property(e => e.IdMedicament).ValueGeneratedNever();
            entity.Property(e => e.CimavetCodinacional).HasColumnName("cimavet_codinacional");
            entity.Property(e => e.CimavetJsondata)
                .IsUnicode(false)
                .HasColumnName("cimavet_jsondata");
            entity.Property(e => e.CimavetNregistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cimavet_nregistro");
            entity.Property(e => e.Nom).HasMaxLength(500);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.TipusCimaVet).HasColumnName("tipusCimaVet");
            entity.Property(e => e.UnitatConcentracio1).HasMaxLength(5);
            entity.Property(e => e.UnitatConcentracio2).HasMaxLength(5);
            entity.Property(e => e.UnitatDosis1).HasMaxLength(5);
            entity.Property(e => e.UnitatDosis2).HasMaxLength(5);
            entity.Property(e => e.ValorConcentracio1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorConcentracio2).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorDosis1).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorDosis2).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<HosMedicamentReceptum>(entity =>
        {
            entity.HasKey(e => e.IdMedicamentRecepta).HasName("PK__Hos_Medi__BB0B24F75614BF03");

            entity.ToTable("Hos_MedicamentRecepta");

            entity.Property(e => e.IdMedicamentRecepta).ValueGeneratedNever();
            entity.Property(e => e.CimavetDosis)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("cimavet_dosis");
            entity.Property(e => e.CimavetDurada).HasColumnName("cimavet_durada");
            entity.Property(e => e.CimavetTipusfrequencia).HasColumnName("cimavet_tipusfrequencia");
            entity.Property(e => e.Dosis).HasMaxLength(100);
            entity.Property(e => e.Durada).HasMaxLength(100);
            entity.Property(e => e.Frequencia).HasMaxLength(100);
            entity.Property(e => e.NomMedicament).HasMaxLength(300);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.QuantitatPrescrita).HasMaxLength(100);

            entity.HasOne(d => d.IdMedicamentNavigation).WithMany(p => p.HosMedicamentRecepta)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_MedicamentRecepta_Hos_Medicament");

            entity.HasOne(d => d.IdReceptaNavigation).WithMany(p => p.HosMedicamentRecepta)
                .HasForeignKey(d => d.IdRecepta)
                .HasConstraintName("FK_Hos_MedicamentRecepta_Hos_Recepta");
        });

        modelBuilder.Entity<HosMotiuHospitalitzacio>(entity =>
        {
            entity.HasKey(e => e.IdMotiuHospitalitzacio).HasName("PK__Hos_Moti__AFD27141071C3F55");

            entity.ToTable("Hos_MotiuHospitalitzacio");

            entity.Property(e => e.IdMotiuHospitalitzacio).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HosPacient>(entity =>
        {
            entity.HasKey(e => e.IdPacient).HasName("PK__Hos_Pacient__658C0CBD");

            entity.ToTable("Hos_Pacient");

            entity.Property(e => e.IdPacient).ValueGeneratedNever();
            entity.Property(e => e.Avis).HasMaxLength(100);
            entity.Property(e => e.AvisIntern).HasMaxLength(200);

            entity.HasOne(d => d.IdHospitalNavigation).WithMany(p => p.HosPacients)
                .HasForeignKey(d => d.IdHospital)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_Pacient_Hos_Hospital");

            entity.HasOne(d => d.IdPacientNavigation).WithOne(p => p.HosPacient)
                .HasForeignKey<HosPacient>(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Pacient_Fac_Client");

            entity.HasOne(d => d.IdPacient1).WithOne(p => p.HosPacient)
                .HasForeignKey<HosPacient>(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Pacient_Slc_Persona");
        });

        modelBuilder.Entity<HosPacientConsultat>(entity =>
        {
            entity.HasKey(e => e.IdPacientConsultat).HasName("PK__Hos_PacientConsu__77AABCF8");

            entity.ToTable("Hos_PacientConsultat");

            entity.HasIndex(e => new { e.IdPacient, e.DiaConsulta }, "IX_Hos_PacientConsultat");

            entity.Property(e => e.IdPacientConsultat).ValueGeneratedNever();
            entity.Property(e => e.DiaConsulta).HasColumnType("datetime");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.HosPacientConsultats)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_PacientConsultat_Hos_Doctor");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosPacientConsultats)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_PacientConsultat_Hos_Pacient");
        });

        modelBuilder.Entity<HosPatologiaPacient>(entity =>
        {
            entity.HasKey(e => e.IdPatologia).HasName("PK__Hos_Pato__6D573A32CAB51D2F");

            entity.ToTable("Hos_PatologiaPacient");

            entity.Property(e => e.IdPatologia).ValueGeneratedNever();
            entity.Property(e => e.DiaAlta).HasColumnType("datetime");
            entity.Property(e => e.Nom).HasMaxLength(50);

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosPatologiaPacients)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_PatologiaPacient_Hos_Pacient");
        });

        modelBuilder.Entity<HosProgramaFillet>(entity =>
        {
            entity.HasKey(e => e.CodiProgramaFillet).HasName("PK__Hos_ProgramaFill__2FEF161B");

            entity.ToTable("Hos_ProgramaFillet");

            entity.Property(e => e.CodiProgramaFillet).ValueGeneratedNever();
        });

        modelBuilder.Entity<HosProva>(entity =>
        {
            entity.HasKey(e => e.IdProva).HasName("PK__Hos_Prova__1446FBA6");

            entity.ToTable("Hos_Prova");

            entity.Property(e => e.IdProva).ValueGeneratedNever();
            entity.Property(e => e.CodiMostra).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdTipusProvaNavigation).WithMany(p => p.HosProvas)
                .HasForeignKey(d => d.IdTipusProva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Prova_Hos_TipusProva");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.HosProvas)
                .HasForeignKey(d => d.IdVisita)
                .HasConstraintName("FK_Hos_Prova_Hos_Visita");
        });

        modelBuilder.Entity<HosReceptum>(entity =>
        {
            entity.HasKey(e => e.IdRecepta).HasName("PK__Hos_Rece__25994533505BE5AD");

            entity.ToTable("Hos_Recepta");

            entity.Property(e => e.IdRecepta).ValueGeneratedNever();
            entity.Property(e => e.DiaRecepta).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.HosRecepta)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Recepta_Hos_Doctor");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosRecepta)
                .HasForeignKey(d => d.IdPacient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Recepta_Hos_Pacient");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.HosRecepta)
                .HasForeignKey(d => d.IdVisita)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Hos_Recepta_Hos_Visita");
        });

        modelBuilder.Entity<HosResultatIdexx>(entity =>
        {
            entity.HasKey(e => e.IdResultatIdexx).HasName("PK__Hos_Resu__253B169D5AF24487");

            entity.ToTable("Hos_ResultatIDEXX");

            entity.Property(e => e.IdResultatIdexx).ValueGeneratedNever();
            entity.Property(e => e.DiaResultat).HasColumnType("datetime");
            entity.Property(e => e.NomFitxer).HasMaxLength(250);

            entity.HasOne(d => d.IdProvaNavigation).WithMany(p => p.HosResultatIdexxes)
                .HasForeignKey(d => d.IdProva)
                .HasConstraintName("FK_Hos_ResultatIDEXX_Hos_Prova");
        });

        modelBuilder.Entity<HosSalaEspera>(entity =>
        {
            entity.HasKey(e => e.IdSalaEspera).HasName("PK__Hos_SalaEspera__2C1E8537");

            entity.ToTable("Hos_SalaEspera");

            entity.Property(e => e.IdSalaEspera).ValueGeneratedNever();
            entity.Property(e => e.HoraEntrada).HasColumnType("datetime");
            entity.Property(e => e.HoraSortida).HasColumnType("datetime");
            entity.Property(e => e.Motiu).HasMaxLength(200);
            entity.Property(e => e.MotiuSortida).HasMaxLength(50);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.HosSalaEsperas)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK_Hos_SalaEspera_Hos_Doctor");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosSalaEsperas)
                .HasForeignKey(d => d.IdPacient)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_SalaEspera_Hos_Pacient");
        });

        modelBuilder.Entity<HosTextVisitum>(entity =>
        {
            entity.HasKey(e => new { e.IdVisita, e.IndexText });

            entity.ToTable("Hos_TextVisita");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.HosTextVisita)
                .HasForeignKey(d => d.IdVisita)
                .HasConstraintName("FK_Hos_TextVisita_Hos_Visita");
        });

        modelBuilder.Entity<HosTipusBaixa>(entity =>
        {
            entity.HasKey(e => e.IdTipusBaixa).HasName("PK__Hos_TipusBaixa__06ED0088");

            entity.ToTable("Hos_TipusBaixa");

            entity.Property(e => e.IdTipusBaixa).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<HosTipusProva>(entity =>
        {
            entity.HasKey(e => e.IdTipusProva).HasName("PK__Hos_TipusProva__0CA5D9DE");

            entity.ToTable("Hos_TipusProva");

            entity.Property(e => e.IdTipusProva).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<HosTipusTipusProva>(entity =>
        {
            entity.HasKey(e => new { e.IdTipusProva, e.Tipus });

            entity.ToTable("Hos_TipusTipusProva");

            entity.Property(e => e.NomDispositiu).HasMaxLength(50);
            entity.Property(e => e.Observacions)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipusProvaNavigation).WithMany(p => p.HosTipusTipusProvas)
                .HasForeignKey(d => d.IdTipusProva)
                .HasConstraintName("FK_Hos_TipusTipusProva_Hos_TipusProva");
        });

        modelBuilder.Entity<HosTipusVacuna>(entity =>
        {
            entity.HasKey(e => e.IdTipusVacuna).HasName("PK__Hos_TipusVacuna__1BE81D6E");

            entity.ToTable("Hos_TipusVacuna");

            entity.Property(e => e.IdTipusVacuna).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasMany(d => d.IdEspecies).WithMany(p => p.IdTipusVacunas)
                .UsingEntity<Dictionary<string, object>>(
                    "VetRelTipusVacunaEspecie",
                    r => r.HasOne<VetEspecie>().WithMany()
                        .HasForeignKey("IdEspecie")
                        .HasConstraintName("FK_VetRel_TipusVacunaEspecie_Vet_Especie"),
                    l => l.HasOne<HosTipusVacuna>().WithMany()
                        .HasForeignKey("IdTipusVacuna")
                        .HasConstraintName("FK_VetRel_TipusVacunaEspecie_Hos_TipusVacuna"),
                    j =>
                    {
                        j.HasKey("IdTipusVacuna", "IdEspecie");
                        j.ToTable("VetRel_TipusVacunaEspecie");
                    });
        });

        modelBuilder.Entity<HosVacuna>(entity =>
        {
            entity.HasKey(e => e.IdVacuna).HasName("PK__Hos_Vacuna__1DD065E0");

            entity.ToTable("Hos_Vacuna");

            entity.Property(e => e.IdVacuna).ValueGeneratedNever();
            entity.Property(e => e.DiaVacuna).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.HosVacunas)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Hos_Vacuna_Fac_Article");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosVacunas)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Vacuna_Hos_Pacient");

            entity.HasOne(d => d.IdTipusVacunaNavigation).WithMany(p => p.HosVacunas)
                .HasForeignKey(d => d.IdTipusVacuna)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Vacuna_Hos_TipusVacuna");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.HosVacunas)
                .HasForeignKey(d => d.IdVisita)
                .HasConstraintName("FK_Hos_Vacuna_Hos_Visita");
        });

        modelBuilder.Entity<HosValorHospitalitzacio>(entity =>
        {
            entity.HasKey(e => e.IdValorHospitalitzacio).HasName("PK__Hos_Valo__199185EF6B8294ED");

            entity.ToTable("Hos_ValorHospitalitzacio");

            entity.Property(e => e.IdValorHospitalitzacio).ValueGeneratedNever();
            entity.Property(e => e.Observacions)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Valor)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDetallHospitalitzacioNavigation).WithMany(p => p.HosValorHospitalitzacios)
                .HasForeignKey(d => d.IdDetallHospitalitzacio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_ValorHospitalitzacio_Hos_DetallHospitalitzacio");
        });

        modelBuilder.Entity<HosValoracioTipusProva>(entity =>
        {
            entity.HasKey(e => e.IdValoracioTipusProva).HasName("PK__Hos_ValoracioTip__116A8EFB");

            entity.ToTable("Hos_ValoracioTipusProva");

            entity.Property(e => e.IdValoracioTipusProva).ValueGeneratedNever();
            entity.Property(e => e.ValorNormalDesde).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorNormalFins).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdDetallTipusProvaNavigation).WithMany(p => p.HosValoracioTipusProvas)
                .HasForeignKey(d => d.IdDetallTipusProva)
                .HasConstraintName("FK_Hos_ValoracioTipusProva_Hos_DetallTipusProva");

            entity.HasOne(d => d.IdReferenciaNavigation).WithMany(p => p.HosValoracioTipusProvas)
                .HasForeignKey(d => d.IdReferencia)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Hos_ValoracioTipusProva_Vet_Especie");
        });

        modelBuilder.Entity<HosVisitum>(entity =>
        {
            entity.HasKey(e => e.IdVisita).HasName("PK__Hos_Visita__7B7B4DDC");

            entity.ToTable("Hos_Visita");

            entity.HasIndex(e => new { e.IdPacient, e.DiaVisita }, "IX_Hos_Visita");

            entity.Property(e => e.IdVisita).ValueGeneratedNever();
            entity.Property(e => e.Alsada).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DiaVisita).HasColumnType("datetime");
            entity.Property(e => e.Pes).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Resum).HasMaxLength(500);

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.HosVisita)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hos_Visita_Hos_Doctor");

            entity.HasOne(d => d.IdPacientNavigation).WithMany(p => p.HosVisita)
                .HasForeignKey(d => d.IdPacient)
                .HasConstraintName("FK_Hos_Visita_Hos_Pacient");
        });

        modelBuilder.Entity<SlcArxiu>(entity =>
        {
            entity.HasKey(e => e.IdArxiu);

            entity.ToTable("Slc_Arxiu");

            entity.Property(e => e.IdArxiu).ValueGeneratedNever();
            entity.Property(e => e.DiaInsercio).HasColumnType("datetime");
            entity.Property(e => e.Extensio).HasMaxLength(5);
            entity.Property(e => e.NomOriginal).HasMaxLength(100);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<SlcArxiuBinari>(entity =>
        {
            entity.HasKey(e => e.IdArxiu);

            entity.ToTable("Slc_ArxiuBinari");

            entity.Property(e => e.IdArxiu).ValueGeneratedNever();

            entity.HasOne(d => d.IdArxiuNavigation).WithOne(p => p.SlcArxiuBinari)
                .HasForeignKey<SlcArxiuBinari>(d => d.IdArxiu)
                .HasConstraintName("FK_Slc_ArxiuBinari_Slc_Arxiu");
        });

        modelBuilder.Entity<SlcBdConfig>(entity =>
        {
            entity.HasKey(e => e.TipusPrograma).HasName("PK__Slc_BdConfig__5A1A5A11");

            entity.ToTable("Slc_BdConfig");

            entity.Property(e => e.TipusPrograma).ValueGeneratedNever();
            entity.Property(e => e.Versio).HasMaxLength(50);
        });

        modelBuilder.Entity<SlcComunicat>(entity =>
        {
            entity.HasKey(e => e.IdComunicat).HasName("PK__Slc_Comu__01650D0A7869D707");

            entity.ToTable("Slc_Comunicat");

            entity.Property(e => e.IdComunicat).ValueGeneratedNever();
            entity.Property(e => e.DiaComunicat).HasColumnType("datetime");
            entity.Property(e => e.DiaCreacio).HasColumnType("datetime");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.SlcComunicats)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_Slc_Comunicat_IdPersona");

            entity.HasOne(d => d.IdUsuariComunicatNavigation).WithMany(p => p.SlcComunicatIdUsuariComunicatNavigations)
                .HasForeignKey(d => d.IdUsuariComunicat)
                .HasConstraintName("FK_Slc_Comunicat_IdUsuariComunicat");

            entity.HasOne(d => d.IdUsuariCreacioNavigation).WithMany(p => p.SlcComunicatIdUsuariCreacioNavigations)
                .HasForeignKey(d => d.IdUsuariCreacio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slc_Comunicat_IdUsuariCreacio");
        });

        modelBuilder.Entity<SlcComunicatInforme>(entity =>
        {
            entity.HasKey(e => e.IdComunicatInforme).HasName("PK__Slc_Comu__2D53FEED10416098");

            entity.ToTable("Slc_ComunicatInforme");

            entity.Property(e => e.IdComunicatInforme).ValueGeneratedNever();
            entity.Property(e => e.DetallsInforme).HasMaxLength(500);

            entity.HasOne(d => d.IdComunicatInformeNavigation).WithOne(p => p.SlcComunicatInforme)
                .HasForeignKey<SlcComunicatInforme>(d => d.IdComunicatInforme)
                .HasConstraintName("FK_Slc_ComunicatInforme_Slc_Comunicat");
        });

        modelBuilder.Entity<SlcComunicatMail>(entity =>
        {
            entity.HasKey(e => e.IdComunicatMail).HasName("PK__Slc_Comu__AB48600204CFADEC");

            entity.ToTable("Slc_ComunicatMail");

            entity.Property(e => e.IdComunicatMail).ValueGeneratedNever();
            entity.Property(e => e.AdresaMail).HasMaxLength(200);

            entity.HasOne(d => d.IdComunicatMailNavigation).WithOne(p => p.SlcComunicatMail)
                .HasForeignKey<SlcComunicatMail>(d => d.IdComunicatMail)
                .HasConstraintName("FK_Slc_ComunicatMail_Slc_Comunicat");

            entity.HasOne(d => d.IdPlantillaMailNavigation).WithMany(p => p.SlcComunicatMails)
                .HasForeignKey(d => d.IdPlantillaMail)
                .HasConstraintName("FK_Slc_ComunicatMail_Slc_Plantilla");
        });

        modelBuilder.Entity<SlcComunicatSm>(entity =>
        {
            entity.HasKey(e => e.IdComunicatSms).HasName("PK__Slc_Comu__882113547F16D496");

            entity.ToTable("Slc_ComunicatSms");

            entity.Property(e => e.IdComunicatSms).ValueGeneratedNever();
            entity.Property(e => e.NumTelefon).HasMaxLength(20);
            entity.Property(e => e.TextSms).HasMaxLength(500);

            entity.HasOne(d => d.IdComunicatSmsNavigation).WithOne(p => p.SlcComunicatSm)
                .HasForeignKey<SlcComunicatSm>(d => d.IdComunicatSms)
                .HasConstraintName("FK_Slc_ComunicatSms_Slc_Comunicat");

            entity.HasOne(d => d.IdPlantillaSmsNavigation).WithMany(p => p.SlcComunicatSms)
                .HasForeignKey(d => d.IdPlantillaSms)
                .HasConstraintName("FK_Slc_ComunicatSms_Slc_Plantilla");
        });

        modelBuilder.Entity<SlcConfiguracio>(entity =>
        {
            entity.HasKey(e => e.IdConfiguracio);

            entity.ToTable("Slc_Configuracio");

            entity.Property(e => e.IdConfiguracio).ValueGeneratedNever();
            entity.Property(e => e.CarpetaDades).HasMaxLength(500);
            entity.Property(e => e.CompteSms).HasMaxLength(50);
            entity.Property(e => e.DiscId).HasMaxLength(100);
            entity.Property(e => e.MailCompteCorreu)
                .HasMaxLength(50)
                .HasColumnName("Mail_CompteCorreu");
            entity.Property(e => e.MailNomAmostrar)
                .HasMaxLength(50)
                .HasColumnName("Mail_NomAMostrar");
            entity.Property(e => e.MailNomHostSmtp)
                .HasMaxLength(50)
                .HasColumnName("Mail_NomHostSmtp");
            entity.Property(e => e.MailPasswordCorreu)
                .HasMaxLength(50)
                .HasColumnName("Mail_PasswordCorreu");
            entity.Property(e => e.MailPort)
                .HasDefaultValue(0, "DF_Slc_Configuracio_Mail_Port")
                .HasColumnName("Mail_Port");
            entity.Property(e => e.MailUsuariCorreu)
                .HasMaxLength(50)
                .HasColumnName("Mail_UsuariCorreu");
            entity.Property(e => e.RemitentSms).HasMaxLength(20);
        });

        modelBuilder.Entity<SlcConnexio>(entity =>
        {
            entity.HasKey(e => e.IdConnexio);

            entity.ToTable("Slc_Connexio");

            entity.Property(e => e.IdConnexio).ValueGeneratedNever();
            entity.Property(e => e.DiaConnexio).HasColumnType("datetime");
            entity.Property(e => e.DiaDesconnexio).HasColumnType("datetime");
            entity.Property(e => e.UsuariSistema).HasMaxLength(50);

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.SlcConnexios)
                .HasForeignKey(d => d.IdMaquina)
                .HasConstraintName("FK_Slc_Connexio_Slc_Maquina");

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.SlcConnexios)
                .HasForeignKey(d => d.IdUsuari)
                .HasConstraintName("FK_Slc_Connexio_Slc_Usuari");
        });

        modelBuilder.Entity<SlcElementAlocat>(entity =>
        {
            entity.HasKey(e => new { e.IdElement, e.NomElement });

            entity.ToTable("Slc_ElementAlocat");

            entity.Property(e => e.NomElement).HasMaxLength(100);
            entity.Property(e => e.HoraAlocat).HasColumnType("datetime");

            entity.HasOne(d => d.IdConnexioNavigation).WithMany(p => p.SlcElementAlocats)
                .HasForeignKey(d => d.IdConnexio)
                .HasConstraintName("FK_Slc_ElementAlocat_Slc_Connexio");
        });

        modelBuilder.Entity<SlcGrup>(entity =>
        {
            entity.HasKey(e => e.IdGrup);

            entity.ToTable("Slc_Grup");

            entity.Property(e => e.IdGrup).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasMany(d => d.IdUsuaris).WithMany(p => p.IdGrups)
                .UsingEntity<Dictionary<string, object>>(
                    "SlcRelGrupUsuari",
                    r => r.HasOne<SlcUsuari>().WithMany()
                        .HasForeignKey("IdUsuari")
                        .HasConstraintName("FK_SlcRel_GrupUsuari_Slc_Usuari"),
                    l => l.HasOne<SlcGrup>().WithMany()
                        .HasForeignKey("IdGrup")
                        .HasConstraintName("FK_SlcRel_GrupUsuari_Slc_Grup"),
                    j =>
                    {
                        j.HasKey("IdGrup", "IdUsuari");
                        j.ToTable("SlcRel_GrupUsuari");
                    });
        });

        modelBuilder.Entity<SlcLlicencium>(entity =>
        {
            entity.HasKey(e => new { e.TipusLlicencia, e.TipusPrograma }).HasName("PF_Slc_Llicencia");

            entity.ToTable("Slc_Llicencia");

            entity.Property(e => e.Checksum).HasMaxLength(200);
            entity.Property(e => e.DiaCaducitat).HasColumnType("datetime");
            entity.Property(e => e.DiscId).HasMaxLength(100);
            entity.Property(e => e.NomSistema).HasMaxLength(13);
        });

        modelBuilder.Entity<SlcMacMaquina>(entity =>
        {
            entity.HasKey(e => e.IdMacMaquina).HasName("PK__Slc_MacM__AB3BB8754B973090");

            entity.ToTable("Slc_MacMaquina");

            entity.Property(e => e.IdMacMaquina).ValueGeneratedNever();
            entity.Property(e => e.AdresaMac).HasMaxLength(50);
            entity.Property(e => e.NomAdresaMac).HasMaxLength(200);

            entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.SlcMacMaquinas)
                .HasForeignKey(d => d.IdMaquina)
                .HasConstraintName("FK_Slc_MacMaquina_Slc_Maquina");
        });

        modelBuilder.Entity<SlcMaquina>(entity =>
        {
            entity.HasKey(e => e.IdMaquina);

            entity.ToTable("Slc_Maquina");

            entity.Property(e => e.IdMaquina).ValueGeneratedNever();
            entity.Property(e => e.AdresaIp)
                .HasMaxLength(50)
                .HasColumnName("AdresaIP");
            entity.Property(e => e.Nom).HasMaxLength(50);

            entity.HasMany(d => d.IdUsuaris).WithMany(p => p.IdMaquinas)
                .UsingEntity<Dictionary<string, object>>(
                    "SlcRelMaquinaUsuari",
                    r => r.HasOne<SlcUsuari>().WithMany()
                        .HasForeignKey("IdUsuari")
                        .HasConstraintName("FK_SlcRel_MaquinaUsuari_Slc_Usuari"),
                    l => l.HasOne<SlcMaquina>().WithMany()
                        .HasForeignKey("IdMaquina")
                        .HasConstraintName("FK_SlcRel_MaquinaUsuari_Slc_Maquina"),
                    j =>
                    {
                        j.HasKey("IdMaquina", "IdUsuari");
                        j.ToTable("SlcRel_MaquinaUsuari");
                    });
        });

        modelBuilder.Entity<SlcNotificacio>(entity =>
        {
            entity.HasKey(e => e.IdNotificacio);

            entity.ToTable("Slc_Notificacio");

            entity.Property(e => e.IdNotificacio).ValueGeneratedNever();
            entity.Property(e => e.DiaCreacio).HasColumnType("datetime");
            entity.Property(e => e.DiaNotificada).HasColumnType("datetime");
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdTipusNotificacioNavigation).WithMany(p => p.SlcNotificacios)
                .HasForeignKey(d => d.IdTipusNotificacio)
                .HasConstraintName("FK_Slc_Notificacio_Slc_TipusNotificacio");

            entity.HasMany(d => d.IdAnimals).WithMany(p => p.IdNotificacios)
                .UsingEntity<Dictionary<string, object>>(
                    "VetNotificacioAnimal",
                    r => r.HasOne<VetAnimal>().WithMany()
                        .HasForeignKey("IdAnimal")
                        .HasConstraintName("FK_Vet_NotificacioAnimal_Vet_Animal"),
                    l => l.HasOne<SlcNotificacio>().WithMany()
                        .HasForeignKey("IdNotificacio")
                        .HasConstraintName("FK_Vet_NotificacioAnimal_Slc_Notificacio"),
                    j =>
                    {
                        j.HasKey("IdNotificacio", "IdAnimal");
                        j.ToTable("Vet_NotificacioAnimal");
                    });

            entity.HasMany(d => d.IdAnimalsNavigation).WithMany(p => p.IdNotificaciosNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "VetNotificacioVacuna",
                    r => r.HasOne<VetAnimal>().WithMany()
                        .HasForeignKey("IdAnimal")
                        .HasConstraintName("FK_Vet_NotificacioVacuna_Vet_Animal"),
                    l => l.HasOne<SlcNotificacio>().WithMany()
                        .HasForeignKey("IdNotificacio")
                        .HasConstraintName("FK_Vet_NotificacioVacuna_Slc_Notificacio"),
                    j =>
                    {
                        j.HasKey("IdNotificacio", "IdAnimal");
                        j.ToTable("Vet_NotificacioVacuna");
                    });

            entity.HasMany(d => d.IdPacients).WithMany(p => p.IdNotificacios)
                .UsingEntity<Dictionary<string, object>>(
                    "HosNotificacioPacient",
                    r => r.HasOne<HosPacient>().WithMany()
                        .HasForeignKey("IdPacient")
                        .HasConstraintName("FK_Hos_NotificacioPacient_Hos_Pacient"),
                    l => l.HasOne<SlcNotificacio>().WithMany()
                        .HasForeignKey("IdNotificacio")
                        .HasConstraintName("FK_Hos_NotificacioPacient_Slc_Notificacio"),
                    j =>
                    {
                        j.HasKey("IdNotificacio", "IdPacient");
                        j.ToTable("Hos_NotificacioPacient");
                    });

            entity.HasMany(d => d.IdPropietaris).WithMany(p => p.IdNotificacios)
                .UsingEntity<Dictionary<string, object>>(
                    "VetNotificacioPropietari",
                    r => r.HasOne<VetPropietari>().WithMany()
                        .HasForeignKey("IdPropietari")
                        .HasConstraintName("FK_Vet_NotificacioPropietari_Vet_Propietari"),
                    l => l.HasOne<SlcNotificacio>().WithMany()
                        .HasForeignKey("IdNotificacio")
                        .HasConstraintName("FK_Vet_NotificacioPropietari_Slc_Notificacio"),
                    j =>
                    {
                        j.HasKey("IdNotificacio", "IdPropietari");
                        j.ToTable("Vet_NotificacioPropietari");
                    });
        });

        modelBuilder.Entity<SlcPersona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.ToTable("Slc_Persona");

            entity.Property(e => e.IdPersona).ValueGeneratedNever();
            entity.Property(e => e.Adresa).HasMaxLength(500);
            entity.Property(e => e.CodiExtern).HasMaxLength(50);
            entity.Property(e => e.CodiPostal).HasMaxLength(5);
            entity.Property(e => e.Cognom1).HasMaxLength(50);
            entity.Property(e => e.Cognom2).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Naixement).HasColumnType("datetime");
            entity.Property(e => e.Nif)
                .HasMaxLength(20)
                .HasColumnName("NIF");
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Pais).HasMaxLength(50);
            entity.Property(e => e.Poblacio).HasMaxLength(50);
            entity.Property(e => e.Provincia).HasMaxLength(50);
        });

        modelBuilder.Entity<SlcPlantillaMail>(entity =>
        {
            entity.HasKey(e => e.IdPlantillaMail).HasName("PK__Slc_Plan__499222C16B0FDBE9");

            entity.ToTable("Slc_PlantillaMail");

            entity.Property(e => e.IdPlantillaMail).ValueGeneratedNever();
            entity.Property(e => e.AdresaFrom).HasMaxLength(200);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.NomFrom).HasMaxLength(200);
        });

        modelBuilder.Entity<SlcPlantillaMailDefecte>(entity =>
        {
            entity.HasKey(e => e.IndexPlantilla).HasName("PK__Slc_Plan__6F73DE0873A521EA");

            entity.ToTable("Slc_PlantillaMailDefecte");

            entity.Property(e => e.IndexPlantilla).ValueGeneratedNever();

            entity.HasOne(d => d.IdPlantillaMailNavigation).WithMany(p => p.SlcPlantillaMailDefectes)
                .HasForeignKey(d => d.IdPlantillaMail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slc_PlantillaMailDefecte_SlcPlantillaMail");
        });

        modelBuilder.Entity<SlcPlantillaSm>(entity =>
        {
            entity.HasKey(e => e.IdPlantillaSms).HasName("PK__Slc_Plan__FB5C76DB673F4B05");

            entity.ToTable("Slc_PlantillaSms");

            entity.Property(e => e.IdPlantillaSms).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.TextSms).HasMaxLength(500);
        });

        modelBuilder.Entity<SlcPlantillaSmsDefecte>(entity =>
        {
            entity.HasKey(e => e.IndexPlantilla).HasName("PK__Slc_Plan__6F73DE086EE06CCD");

            entity.ToTable("Slc_PlantillaSmsDefecte");

            entity.Property(e => e.IndexPlantilla).ValueGeneratedNever();

            entity.HasOne(d => d.IdPlantillaSmsNavigation).WithMany(p => p.SlcPlantillaSmsDefectes)
                .HasForeignKey(d => d.IdPlantillaSms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slc_PlantillaSmsDefecte_SlcPlantillaSms");
        });

        modelBuilder.Entity<SlcPrivilegi>(entity =>
        {
            entity.HasKey(e => e.IdPrivilegi);

            entity.ToTable("Slc_Privilegi");

            entity.Property(e => e.IdPrivilegi).ValueGeneratedNever();

            entity.HasMany(d => d.IdGrups).WithMany(p => p.IdPrivilegis)
                .UsingEntity<Dictionary<string, object>>(
                    "SlcRelPrivilegiGrup",
                    r => r.HasOne<SlcGrup>().WithMany()
                        .HasForeignKey("IdGrup")
                        .HasConstraintName("FK_SlcRel_PrivilegiGrup_Slc_Grup"),
                    l => l.HasOne<SlcPrivilegi>().WithMany()
                        .HasForeignKey("IdPrivilegi")
                        .HasConstraintName("FK_SlcRel_PrivilegiGrup_Slc_Privilegi"),
                    j =>
                    {
                        j.HasKey("IdPrivilegi", "IdGrup");
                        j.ToTable("SlcRel_PrivilegiGrup");
                    });

            entity.HasMany(d => d.IdUsuaris).WithMany(p => p.IdPrivilegis)
                .UsingEntity<Dictionary<string, object>>(
                    "SlcRelPrivilegiUsuari",
                    r => r.HasOne<SlcUsuari>().WithMany()
                        .HasForeignKey("IdUsuari")
                        .HasConstraintName("FK_SlcRel_PrivilegiUsuari_Slc_Usuari"),
                    l => l.HasOne<SlcPrivilegi>().WithMany()
                        .HasForeignKey("IdPrivilegi")
                        .HasConstraintName("FK_SlcRel_PrivilegiUsuari_Slc_Privilegi"),
                    j =>
                    {
                        j.HasKey("IdPrivilegi", "IdUsuari");
                        j.ToTable("SlcRel_PrivilegiUsuari");
                    });
        });

        modelBuilder.Entity<SlcRecordatori>(entity =>
        {
            entity.HasKey(e => e.IdRecordatori);

            entity.ToTable("Slc_Recordatori");

            entity.Property(e => e.IdRecordatori).ValueGeneratedNever();
            entity.Property(e => e.HoraAvis).HasColumnType("datetime");
            entity.Property(e => e.HoraCreacio).HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(4000);

            entity.HasOne(d => d.IdUsuariNavigation).WithMany(p => p.SlcRecordatoris)
                .HasForeignKey(d => d.IdUsuari)
                .HasConstraintName("FK_Slc_Recordatori_Slc_Usuari");
        });

        modelBuilder.Entity<SlcRelacionsExterne>(entity =>
        {
            entity.HasKey(e => e.IdRelacioExterna).HasName("PK__Slc_Rela__48866BF718D6A699");

            entity.ToTable("Slc_RelacionsExternes");

            entity.Property(e => e.IdRelacioExterna)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipusElem)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SlcSobre>(entity =>
        {
            entity.HasKey(e => e.CodiSistema).HasName("PK_Slc_Sobre_1");

            entity.ToTable("Slc_Sobre");

            entity.Property(e => e.CodiSistema).ValueGeneratedNever();
        });

        modelBuilder.Entity<SlcTelefon>(entity =>
        {
            entity.HasKey(e => e.IdTelefon);

            entity.ToTable("Slc_Telefon");

            entity.Property(e => e.IdTelefon).ValueGeneratedNever();
            entity.Property(e => e.Numero).HasMaxLength(20);
            entity.Property(e => e.Observacions).HasMaxLength(100);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.SlcTelefons)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK_Slc_Telefon_Slc_Persona");
        });

        modelBuilder.Entity<SlcTextNotificacio>(entity =>
        {
            entity.HasKey(e => e.IdNotificacio).HasName("PK__Slc_TextNotifica__60C757A0");

            entity.ToTable("Slc_TextNotificacio");

            entity.Property(e => e.IdNotificacio).ValueGeneratedNever();

            entity.HasOne(d => d.IdNotificacioNavigation).WithOne(p => p.SlcTextNotificacio)
                .HasForeignKey<SlcTextNotificacio>(d => d.IdNotificacio)
                .HasConstraintName("FK_Slc_TextNotificacio_Slc_Notificacio");
        });

        modelBuilder.Entity<SlcTipusNotificacio>(entity =>
        {
            entity.HasKey(e => e.IdTipusNotificacio);

            entity.ToTable("Slc_TipusNotificacio");

            entity.Property(e => e.IdTipusNotificacio).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<SlcTipusNotificacioCartum>(entity =>
        {
            entity.HasKey(e => e.IdTipusNotificacio).HasName("PK_Slc_NotificacioWord");

            entity.ToTable("Slc_TipusNotificacio_Carta");

            entity.Property(e => e.IdTipusNotificacio).ValueGeneratedNever();

            entity.HasOne(d => d.IdTipusNotificacioNavigation).WithOne(p => p.SlcTipusNotificacioCartum)
                .HasForeignKey<SlcTipusNotificacioCartum>(d => d.IdTipusNotificacio)
                .HasConstraintName("FK_Slc_TipusNotificacio_Word_Slc_TipusNotificacio");
        });

        modelBuilder.Entity<SlcTipusNotificacioMail>(entity =>
        {
            entity.HasKey(e => e.IdTipusNotificacio).HasName("PK_Slc_NotificacioMail");

            entity.ToTable("Slc_TipusNotificacio_Mail");

            entity.Property(e => e.IdTipusNotificacio).ValueGeneratedNever();
            entity.Property(e => e.PlantillaMail).HasMaxLength(100);

            entity.HasOne(d => d.IdTipusNotificacioNavigation).WithOne(p => p.SlcTipusNotificacioMail)
                .HasForeignKey<SlcTipusNotificacioMail>(d => d.IdTipusNotificacio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slc_TipusNotificacio_Mail_Slc_TipusNotificacio");
        });

        modelBuilder.Entity<SlcTipusNotificacioSm>(entity =>
        {
            entity.HasKey(e => e.IdTipusNotificacio).HasName("PK_Slc_NotificacioSms");

            entity.ToTable("Slc_TipusNotificacio_Sms");

            entity.Property(e => e.IdTipusNotificacio).ValueGeneratedNever();
            entity.Property(e => e.TextSms).HasMaxLength(1000);

            entity.HasOne(d => d.IdTipusNotificacioNavigation).WithOne(p => p.SlcTipusNotificacioSm)
                .HasForeignKey<SlcTipusNotificacioSm>(d => d.IdTipusNotificacio)
                .HasConstraintName("FK_Slc_TipusNotificacio_Sms_Slc_TipusNotificacio");
        });

        modelBuilder.Entity<SlcUsuari>(entity =>
        {
            entity.HasKey(e => e.IdUsuari);

            entity.ToTable("Slc_Usuari");

            entity.Property(e => e.IdUsuari)
                .ValueGeneratedNever()
                .HasComment("Identificador de l'usuari");
            entity.Property(e => e.Codi).HasMaxLength(50);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<VetAnimal>(entity =>
        {
            entity.HasKey(e => e.IdAnimal);

            entity.ToTable("Vet_Animal");

            entity.Property(e => e.IdAnimal).ValueGeneratedNever();
            entity.Property(e => e.Capa).HasMaxLength(50);
            entity.Property(e => e.Caracter).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.DiaCel).HasColumnType("datetime");
            entity.Property(e => e.NumXip).HasMaxLength(50);
            entity.Property(e => e.Tatuatge).HasMaxLength(50);

            entity.HasOne(d => d.IdAnimalNavigation).WithOne(p => p.VetAnimal)
                .HasForeignKey<VetAnimal>(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_Animal_Hos_Pacient");

            entity.HasOne(d => d.IdPropietariNavigation).WithMany(p => p.VetAnimals)
                .HasForeignKey(d => d.IdPropietari)
                .HasConstraintName("FK_Vet_Animal_Vet_Propietari");

            entity.HasOne(d => d.IdRasaNavigation).WithMany(p => p.VetAnimals)
                .HasForeignKey(d => d.IdRasa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_Animal_Vet_Rasa");
        });

        modelBuilder.Entity<VetConfiguracio>(entity =>
        {
            entity.HasKey(e => e.IdConfiguracio).HasName("PK__Vet_Conf__BB3704FD1CA7377D");

            entity.ToTable("Vet_Configuracio");

            entity.Property(e => e.IdConfiguracio).ValueGeneratedNever();
        });

        modelBuilder.Entity<VetEspecie>(entity =>
        {
            entity.HasKey(e => e.IdEspecie);

            entity.ToTable("Vet_Especie");

            entity.Property(e => e.IdEspecie).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);
        });

        modelBuilder.Entity<VetPesTaula>(entity =>
        {
            entity.HasKey(e => e.IdPesTaula);

            entity.ToTable("Vet_PesTaula");

            entity.Property(e => e.IdPesTaula).ValueGeneratedNever();
            entity.Property(e => e.Pes).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.IdTaulaPesNavigation).WithMany(p => p.VetPesTaulas)
                .HasForeignKey(d => d.IdTaulaPes)
                .HasConstraintName("FK_Vet_PesTaula_Vet_TaulaPes");
        });

        modelBuilder.Entity<VetPropietari>(entity =>
        {
            entity.HasKey(e => e.IdPropietari);

            entity.ToTable("Vet_Propietari");

            entity.Property(e => e.IdPropietari).ValueGeneratedNever();

            entity.HasOne(d => d.IdPropietariNavigation).WithOne(p => p.VetPropietari)
                .HasForeignKey<VetPropietari>(d => d.IdPropietari)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_Propietari_Fac_Client");

            entity.HasOne(d => d.IdPropietari1).WithOne(p => p.VetPropietari)
                .HasForeignKey<VetPropietari>(d => d.IdPropietari)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_Propietari_Slc_Persona");
        });

        modelBuilder.Entity<VetPropietariAntic>(entity =>
        {
            entity.HasKey(e => e.IdPropietariAntic).HasName("PK_Vet_PropietariAntic_1");

            entity.ToTable("Vet_PropietariAntic");

            entity.Property(e => e.IdPropietariAntic).ValueGeneratedNever();
            entity.Property(e => e.DiaCanvi).HasColumnType("datetime");
            entity.Property(e => e.MotiuCanvi).HasMaxLength(100);

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.VetPropietariAntics)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_PropietariAntic_Vet_Animal");

            entity.HasOne(d => d.IdPropietariNavigation).WithMany(p => p.VetPropietariAntics)
                .HasForeignKey(d => d.IdPropietari)
                .HasConstraintName("FK_Vet_PropietariAntic_Vet_Propietari");
        });

        modelBuilder.Entity<VetRasa>(entity =>
        {
            entity.HasKey(e => e.IdRasa);

            entity.ToTable("Vet_Rasa");

            entity.Property(e => e.IdRasa).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.VetRasas)
                .HasForeignKey(d => d.IdEspecie)
                .HasConstraintName("FK_Vet_Rasa_Vet_Especie");
        });

        modelBuilder.Entity<VetTaulaPe>(entity =>
        {
            entity.HasKey(e => e.IdTaulaPes);

            entity.ToTable("Vet_TaulaPes");

            entity.Property(e => e.IdTaulaPes).ValueGeneratedNever();
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Observacions).HasMaxLength(500);

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.VetTaulaPes)
                .HasForeignKey(d => d.IdEspecie)
                .HasConstraintName("FK_Vet_TaulaPes_Vet_Especie");
        });

        modelBuilder.Entity<VetTextVisitum>(entity =>
        {
            entity.HasKey(e => new { e.IdVisita, e.IndexText });

            entity.ToTable("Vet_TextVisita");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.VetTextVisita)
                .HasForeignKey(d => d.IdVisita)
                .HasConstraintName("FK_Vet_TextVisita_Vet_Visita");
        });

        modelBuilder.Entity<VetVisitum>(entity =>
        {
            entity.HasKey(e => e.IdVisita);

            entity.ToTable("Vet_Visita");

            entity.Property(e => e.IdVisita).ValueGeneratedNever();

            entity.HasOne(d => d.IdVisitaNavigation).WithOne(p => p.VetVisitum)
                .HasForeignKey<VetVisitum>(d => d.IdVisita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vet_Visita_Hos_Visita");
        });

        modelBuilder.Entity<VetVistaDarrerPe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VetVista_DarrerPes");

            entity.Property(e => e.Pes).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<VetVistaDarreraAlsadum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VetVista_DarreraAlsada");

            entity.Property(e => e.Alsada).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<VetVistaDarreraVisitum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VetVista_DarreraVisita");

            entity.Property(e => e.DarrerDia).HasColumnType("datetime");
        });

        modelBuilder.Entity<VetVistaDeute>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VetVista_Deute");

            entity.Property(e => e.Total).HasColumnType("decimal(38, 2)");
        });

        modelBuilder.Entity<VetVistaPerruquery>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VetVista_Perruqueries");

            entity.Property(e => e.Cognoms).HasMaxLength(101);
            entity.Property(e => e.Concepte).HasMaxLength(100);
            entity.Property(e => e.DiaVenda)
                .HasColumnType("datetime")
                .HasColumnName("Dia Venda");
            entity.Property(e => e.NomMascota)
                .HasMaxLength(50)
                .HasColumnName("Nom Mascota");
            entity.Property(e => e.Preu).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Torn)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VistaArticlesActiu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaArticlesActius");

            entity.Property(e => e.CodiBarres)
                .HasMaxLength(50)
                .HasColumnName("Codi Barres");
            entity.Property(e => e.CodiPropi)
                .HasMaxLength(50)
                .HasColumnName("Codi Propi");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.NomArticle)
                .HasMaxLength(100)
                .HasColumnName("Nom Article");
            entity.Property(e => e.NomEmpresa)
                .HasMaxLength(50)
                .HasColumnName("Nom Empresa");
            entity.Property(e => e.NomFamilia)
                .HasMaxLength(50)
                .HasColumnName("Nom Familia");
            entity.Property(e => e.PreuNet)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("Preu Net");
            entity.Property(e => e.Pvp)
                .HasColumnType("decimal(38, 8)")
                .HasColumnName("PVP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
