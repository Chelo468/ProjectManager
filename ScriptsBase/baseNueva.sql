USE [testingProyectos]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[login_name] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[email] [varchar](100) NOT NULL,
	[token_clave] [varchar](50) NULL,
	[habilitado] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado_Publicacion]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado_Publicacion](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Estado_Publicacion] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado_Defecto]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado_Defecto](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Estado_Defecto] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Publicaciones]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Publicaciones](
	[id_publicacion] [int] IDENTITY(1,1) NOT NULL,
	[id_proyecto] [int] NULL,
	[id_usuario_publicador] [int] NULL,
	[id_usuario_evaluador] [int] NULL,
	[version] [varchar](50) NULL,
	[archivo_devolucion] [image] NULL,
	[fecha_estimada_resolucion] [datetime] NULL,
	[fecha_publicacion] [datetime] NULL,
	[fecha_devolucion] [datetime] NULL,
	[id_estado] [int] NULL,
 CONSTRAINT [PK_Publicaciones] PRIMARY KEY CLUSTERED 
(
	[id_publicacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proyectos](
	[id_proyecto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fecha_alta] [datetime] NOT NULL,
	[fecha_ultima_modif] [datetime] NOT NULL,
	[id_usuario_creador] [int] NOT NULL,
	[descripcion] [varchar](500) NULL,
	[urlTesting] [varchar](200) NULL,
	[urlProduccion] [varchar](200) NULL,
	[fecha_baja] [datetime] NULL,
 CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED 
(
	[id_proyecto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles_Usuarios](
	[id_usuario] [int] NOT NULL,
	[id_rol] [int] NOT NULL,
 CONSTRAINT [PK_Roles_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_rol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Defectos]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Defectos](
	[id_defecto] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario_detector] [int] NULL,
	[id_usuario_resolutor] [int] NULL,
	[descripcion] [varchar](200) NULL,
	[id_estado] [int] NULL,
	[id_publicacion_resolucion] [int] NULL,
	[fecha_deteccion] [datetime] NULL,
	[fecha_resolucion] [datetime] NULL,
 CONSTRAINT [PK_Defectos] PRIMARY KEY CLUSTERED 
(
	[id_defecto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proyectos_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyectos_Usuarios](
	[id_proyecto] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_rol] [int] NOT NULL,
	[fecha_desde] [datetime] NULL,
 CONSTRAINT [PK_Proyectos_Usuarios] PRIMARY KEY CLUSTERED 
(
	[id_proyecto] ASC,
	[id_usuario] ASC,
	[id_rol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sesiones]    Script Date: 03/16/2018 17:58:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sesiones](
	[id_usuario] [int] NOT NULL,
	[id_rol] [int] NULL,
	[fecha_inicio] [datetime] NULL,
	[token] [varchar](100) NULL,
 CONSTRAINT [PK_Sesiones] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usuariosGetById]    Script Date: 03/16/2018 17:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela, Marcelo
-- Create date: 01/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[usuariosGetById]
	@id_usuario int
AS
BEGIN
	SELECT *
	FROM Usuarios
	WHERE id_usuario = @id_usuario
END
GO
/****** Object:  StoredProcedure [dbo].[usuariosGetByUserNamePassword]    Script Date: 03/16/2018 17:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 08/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[usuariosGetByUserNamePassword]
	@login_name varchar(50),
	@password varchar (100)
AS
BEGIN
	SELECT * 
	FROM Usuarios
	WHERE UPPER(login_name) = UPPER(LTRIM(RTRIM(@login_name)))
	and password = LTRIM(RTRIM(@password))
END
GO
/****** Object:  StoredProcedure [dbo].[proyectosGetByIdUser]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 13/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[proyectosGetByIdUser]
	@idUser int
AS
BEGIN
	SELECT *
	FROM Proyectos p
	LEFT JOIN Proyectos_Usuarios pu
	ON p.id_proyecto = pu.id_proyecto
	where pu.id_usuario = @idUser
	or p.id_usuario_creador = @idUser
END
GO
/****** Object:  StoredProcedure [dbo].[proyectosInsert]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 16/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[proyectosInsert]
	@nombre varchar(50),
	@descripcion varchar(500),
	@urlTesting varchar(200),
	@urlProduccion varchar(200),
	@id_usuario_creador int,
	@fecha_alta datetime
AS
BEGIN
	
	INSERT INTO Proyectos
		(nombre, descripcion, urlTesting, urlProduccion, id_usuario_creador, fecha_alta, fecha_ultima_modif)
	VALUES
		(@nombre,@descripcion,@urlTesting,@urlProduccion,@id_usuario_creador, @fecha_alta, @fecha_alta);
		
	SELECT @@IDENTITY
	
END
GO
/****** Object:  StoredProcedure [dbo].[proyectosGetByIdProyectoIdUser]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 13/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[proyectosGetByIdProyectoIdUser]
	@idUser int,
	@idProyecto int
AS
BEGIN
	SELECT *
	FROM Proyectos p
	LEFT JOIN Proyectos_Usuarios pu
	ON p.id_proyecto = pu.id_proyecto
	where p.id_usuario_creador = @idUser
	and p.id_proyecto = @idProyecto
END
GO
/****** Object:  StoredProcedure [dbo].[proyectosUpdate]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 13/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[proyectosUpdate]
	@id_proyecto int,
	@nombre varchar(50),
	@descripcion varchar(500),
	@fecha_ultima_modif datetime,
	@urltesting varchar(200),
	@urlProduccion varchar(200)
AS
BEGIN
	UPDATE Proyectos
	set 
		nombre = @nombre,
		descripcion = @descripcion,
		urlTesting = @urlTesting,
		urlProduccion = @urlProduccion,
		fecha_ultima_modif = @fecha_ultima_modif
		where id_proyecto = @id_proyecto
	
END
GO
/****** Object:  StoredProcedure [dbo].[sesionesGetByIdUser]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 16/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[sesionesGetByIdUser]
	@id_usuario int
AS
BEGIN
	SELECT *
	FROM Sesiones
	WHERE id_usuario = @id_usuario
END
GO
/****** Object:  StoredProcedure [dbo].[sesionesInsert]    Script Date: 03/16/2018 17:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 16/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[sesionesInsert]
	@id_usuario int,
	@fecha_inicio datetime,
	@token varchar(100)
AS
BEGIN
	INSERT INTO Sesiones
		(id_usuario, fecha_inicio, token)
	VALUES
		(@id_usuario, @fecha_inicio, @token)
END
GO
/****** Object:  StoredProcedure [dbo].[sesionesDelete]    Script Date: 03/16/2018 17:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 16/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[sesionesDelete]
	@id_usuario int
AS
BEGIN
	DELETE Sesiones
	where id_usuario = @id_usuario
END
GO
/****** Object:  StoredProcedure [dbo].[sesionesGetByToken]    Script Date: 03/16/2018 17:58:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Brizuela Marcelo
-- Create date: 16/03/2018
-- =============================================
CREATE PROCEDURE [dbo].[sesionesGetByToken]
	@token varchar(100)
AS
BEGIN
	SELECT *
	FROM Sesiones
	where token = @token
END
GO
/****** Object:  ForeignKey [FK_Defectos_Estado_Defecto]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Defectos]  WITH CHECK ADD  CONSTRAINT [FK_Defectos_Estado_Defecto] FOREIGN KEY([id_estado])
REFERENCES [dbo].[Estado_Defecto] ([id_estado])
GO
ALTER TABLE [dbo].[Defectos] CHECK CONSTRAINT [FK_Defectos_Estado_Defecto]
GO
/****** Object:  ForeignKey [FK_Defectos_Publicaciones]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Defectos]  WITH CHECK ADD  CONSTRAINT [FK_Defectos_Publicaciones] FOREIGN KEY([id_publicacion_resolucion])
REFERENCES [dbo].[Publicaciones] ([id_publicacion])
GO
ALTER TABLE [dbo].[Defectos] CHECK CONSTRAINT [FK_Defectos_Publicaciones]
GO
/****** Object:  ForeignKey [FK_Defectos_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Defectos]  WITH CHECK ADD  CONSTRAINT [FK_Defectos_Usuarios] FOREIGN KEY([id_usuario_detector])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Defectos] CHECK CONSTRAINT [FK_Defectos_Usuarios]
GO
/****** Object:  ForeignKey [FK_Defectos_Usuarios1]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Defectos]  WITH CHECK ADD  CONSTRAINT [FK_Defectos_Usuarios1] FOREIGN KEY([id_usuario_resolutor])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Defectos] CHECK CONSTRAINT [FK_Defectos_Usuarios1]
GO
/****** Object:  ForeignKey [FK_Proyectos_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Proyectos]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_Usuarios] FOREIGN KEY([id_usuario_creador])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Proyectos] CHECK CONSTRAINT [FK_Proyectos_Usuarios]
GO
/****** Object:  ForeignKey [FK_Proyectos_Usuarios_Proyectos]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Proyectos_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_Usuarios_Proyectos] FOREIGN KEY([id_proyecto])
REFERENCES [dbo].[Proyectos] ([id_proyecto])
GO
ALTER TABLE [dbo].[Proyectos_Usuarios] CHECK CONSTRAINT [FK_Proyectos_Usuarios_Proyectos]
GO
/****** Object:  ForeignKey [FK_Proyectos_Usuarios_Roles_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Proyectos_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Proyectos_Usuarios_Roles_Usuarios] FOREIGN KEY([id_usuario], [id_rol])
REFERENCES [dbo].[Roles_Usuarios] ([id_usuario], [id_rol])
GO
ALTER TABLE [dbo].[Proyectos_Usuarios] CHECK CONSTRAINT [FK_Proyectos_Usuarios_Roles_Usuarios]
GO
/****** Object:  ForeignKey [FK_Publicaciones_Estado_Publicacion]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Publicaciones]  WITH CHECK ADD  CONSTRAINT [FK_Publicaciones_Estado_Publicacion] FOREIGN KEY([id_estado])
REFERENCES [dbo].[Estado_Publicacion] ([id_estado])
GO
ALTER TABLE [dbo].[Publicaciones] CHECK CONSTRAINT [FK_Publicaciones_Estado_Publicacion]
GO
/****** Object:  ForeignKey [FK_Publicaciones_Proyectos]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Publicaciones]  WITH CHECK ADD  CONSTRAINT [FK_Publicaciones_Proyectos] FOREIGN KEY([id_proyecto])
REFERENCES [dbo].[Proyectos] ([id_proyecto])
GO
ALTER TABLE [dbo].[Publicaciones] CHECK CONSTRAINT [FK_Publicaciones_Proyectos]
GO
/****** Object:  ForeignKey [FK_Publicaciones_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Publicaciones]  WITH CHECK ADD  CONSTRAINT [FK_Publicaciones_Usuarios] FOREIGN KEY([id_usuario_publicador])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Publicaciones] CHECK CONSTRAINT [FK_Publicaciones_Usuarios]
GO
/****** Object:  ForeignKey [FK_Publicaciones_Usuarios1]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Publicaciones]  WITH CHECK ADD  CONSTRAINT [FK_Publicaciones_Usuarios1] FOREIGN KEY([id_usuario_evaluador])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Publicaciones] CHECK CONSTRAINT [FK_Publicaciones_Usuarios1]
GO
/****** Object:  ForeignKey [FK_Roles_Usuarios_Roles]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Roles_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Usuarios_Roles] FOREIGN KEY([id_rol])
REFERENCES [dbo].[Roles] ([id_rol])
GO
ALTER TABLE [dbo].[Roles_Usuarios] CHECK CONSTRAINT [FK_Roles_Usuarios_Roles]
GO
/****** Object:  ForeignKey [FK_Roles_Usuarios_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Roles_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Usuarios_Usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Roles_Usuarios] CHECK CONSTRAINT [FK_Roles_Usuarios_Usuarios]
GO
/****** Object:  ForeignKey [FK_Sesiones_Roles_Usuarios]    Script Date: 03/16/2018 17:58:23 ******/
ALTER TABLE [dbo].[Sesiones]  WITH CHECK ADD  CONSTRAINT [FK_Sesiones_Roles_Usuarios] FOREIGN KEY([id_usuario], [id_rol])
REFERENCES [dbo].[Roles_Usuarios] ([id_usuario], [id_rol])
GO
ALTER TABLE [dbo].[Sesiones] CHECK CONSTRAINT [FK_Sesiones_Roles_Usuarios]
GO
