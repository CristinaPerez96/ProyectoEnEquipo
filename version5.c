#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

typedef struct{
	int socket;
}Socket;

typedef struct{
	Socket sockets[100];
	int num;
}ListaSockets;

typedef struct {
	char nombre[20];
	int socket;
} Conectado;

typedef struct{
	Conectado conectados[100];
	int num;
} ListaConectados;

int contadorservicios;
ListaConectados miLista;
ListaSockets misSockets;
//ListaSockets listaActivos;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int PonSocket(ListaSockets *lista, int socket)
{
	//Añade un nuevo socket a la lista misSockets
	if(lista->num == 100)
		return -1;
	else
	{
		pthread_mutex_lock(&mutex);//No me interrumpas ahora
		lista->sockets[lista->num].socket=socket;
		lista->num++;
		pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
		return 0;
		
	}
}

int EliminaSocket (ListaSockets *lista, int socket)
{
	//Elimina el socket de la lista misSockets
	int pos=DamePosicion (lista, socket);
	if(pos==-1)
		return -1;
	else
	{
		int i;
		pthread_mutex_lock(&mutex);//No me interrumpas ahora
		for(i=pos; i< lista->num-1; i++)
		{
			lista->sockets[i]=lista->sockets[i+1];
			//strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
			//lista->conectados[i].socket=lista->conectados[i+1].socket;
		}
		lista->num--;
		pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
		return 0;
	}
}




void DameSockets(ListaSockets *lista, char sockets[100]){
	//Pone en conectados los nombres de todos los conectados separados
	//por /. Primero pone el número de conectados
	//Ejemplo: "3/Pedro/Mayra/Luis"
	
	sprintf(sockets,"%d", lista->num);
	int i;
	pthread_mutex_lock(&mutex);//No me interrumpas ahora
	for(i=0;i<lista->num;i++)
		sprintf(sockets, "%s/%d", sockets, lista->sockets[i].socket);
	pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
	
}

//La lista se pasa por referencia
int PonConectado (ListaConectados *lista, char nombre[20], int socket){
	//añade nuevo conectado y retorna 0 si okey o 0 si la lista ya estaba llena
	if(lista->num ==100)
		return -1;
	else
	{
		pthread_mutex_lock(&mutex);//No me interrumpas ahora
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket=socket;
		lista->num++;
		pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
		return 0;
	}
}

int EliminaConectado (ListaConectados *lista, char nombre[20]){
	//Retorna 0 si se ha eliminado y -1 si el usuario no esta en la lista
	//lista ya es un puntero a la lista y por ello la pongo tal cual sin &
	int pos=DamePosicion (lista, nombre);
	if(pos==-1)
		return -1;
	else
	{
		int i;
		pthread_mutex_lock(&mutex);//No me interrumpas ahora
		for(i=pos; i< lista->num-1; i++)
		{
			lista->conectados[i]=lista->conectados[i+1];
			//strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
			//lista->conectados[i].socket=lista->conectados[i+1].socket;
		}
		lista->num--;
		pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
		return 0;
	}
	
}

void DameConectados(ListaConectados *lista, char conectados[300]){
	//Pone en conectados los nombres de todos los conectados separados
	//por /. Primero pone el número de conectados
	//Ejemplo: "3/Pedro/Mayra/Luis"
	pthread_mutex_lock(&mutex);//No me interrumpas ahora
	sprintf(conectados,"%d", lista->num);
	int i;
	for(i=0;i<lista->num;i++)
		sprintf(conectados, "%s/%s", conectados, lista->conectados[i].nombre);
	pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
	
}

int DamePosicion(ListaConectados *lista, char nombre[20]){
	//Devuelve el socket o -1 si no lo ha encontrado en la lista
	//esquema de busqueda
	int i=0;
	int encontrado=0;
	while((i<lista->num)&& (!encontrado))
	{
		if(strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado=1;
		else
			i++;
	}
	if(encontrado)
							return i;
	else
		return -1;
}

void DameUser(ListaConectados *lista, int socket, char nombre[20]){
	//Devuelve el socket o -1 si no lo ha encontrado en la lista
	//esquema de busqueda
	int i=0;
	int encontrado=0;
	while((i<lista->num)&& (!encontrado))
	{
		if(lista->conectados[i].socket==socket)
			encontrado=1;
		else
			i++;
	}
	if(encontrado)
							sprintf(nombre, "%s", lista->conectados[i].nombre);
	else
		printf("No se ha encontrado ningún usuario con ese socket");
}

//int Inapropiada (char frase[100])
//{
	//NOs devolvera -1 si la frase es inapropaida y 0 si la frase se acepta como OK
	//if (frase[0] == 'A')
		//return -1;
	//else 
		//return 0;
//}

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int*) socket;
	sock_conn= *s;
	//int socket_conn= * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	char respuesta1[512];
	char palabra[20];
	char notificacion[512];
	int ret;
	int res;
	int terminar=0;
	char misconectados[300];
	//Entramos en un bucle para atender todas las peticiones de este clientes
	//hasta que se desconecte
	while(terminar==0)
	{
		//Ahora recibimos la peticion
		ret=read(sock_conn, peticion, sizeof(peticion));
		printf("Recibido\n");
		
		//Tenemos que añadirle la marca de fin de string
		//para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		printf("Peticion: %s\n", peticion);
		//vamos a ver que nos pide el cliente
		char *p= strtok(peticion,"/");
		int codigo= atoi(p);
		//ya tenemos el codigo de peticion
		char user[20];
		char password[20];
		char invitado[20];
		char invitador[20];
		int err;
		int id;
		char idultimo[10];
		char consulta[80];
		MYSQL *conn;
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		
		
		if((codigo==1)||(codigo==2)||(codigo==4)||(codigo==5))
		{
			p=strtok(NULL,"/");
			strcpy(user,p);
			//ya tenemos el nombre de usuario
			printf("Codigo: %d, Nombre: %s\n", codigo, user);
		}
		if((codigo!=0))
		{
			conn=mysql_init(NULL);
			if(conn==NULL)
			{
				printf("Error al crear la conexion: %u %s\n",
					   mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//inicializar la conexi??n, entrando nuestras claves de acceso y
			//el nombre de la base de datos a la que queremos acceder 
			conn = mysql_real_connect (conn, "localhost","root", "mysql", "T3_Juego",0, NULL, 0);
			if (conn==NULL) 
			{
				printf ("Error al inicializar la conexion: %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
		}
		
		if(codigo==0)
		//peticion de desconexion
			terminar=1;
		
		else if(codigo==1) //Registra un usuario
		{
			p = strtok (NULL, "/");
			strcpy (password, p);
			
			// construimos la consulta SQL para saber cual es el último id ocupado
			strcpy (consulta,"SELECT MAX(jugador.id) FROM jugador "); 
			
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "1/No se ha podido realizar la consulta");
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "1/No se han obtenido datos la consulta");
			}
			else
				id=atoi(row[0]);
			// El resultado debe ser una matriz con una sola fila
			// y una columna que contiene el id
			
			int id1=id +1;
			sprintf(idultimo, "%d", id1);
			printf("User: %s con password: %s quiere registrarse en la BBDD\n", user, password);
			
			
			// Ahora construimos el string con el comando SQL
			// para insertar la persona en la base. Ese string es:
			// INSERT INTO personas VALUES ('dni', 'nombre', edad); 
			strcpy (consulta, "INSERT INTO T3_Juego.jugador VALUES (");
			//concatenamos el id
			strcat (consulta, idultimo);
			strcat (consulta,",'");
			//concatenamos el user 
			strcat (consulta, user); 
			strcat (consulta, "','");
			//concatenamos el password 
			strcat (consulta, password); 
			strcat (consulta, "');");
			
			
			// Ahora ya podemos realizar la insercion 
			err = mysql_query(conn, consulta);
			if (err!=0) 
			{
				printf ("Error al introducir datos la base %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "1/No se ha podido realizar la consulta");
				exit (1);
			}
			
			//Vemos si se ha añadido a la tabla jugador para ello la imprimimos
			err=mysql_query (conn, "SELECT * FROM jugador");
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "1/Error al consultar la base de datos");
				exit (1);
			}
			//recogemos el resultado de la consulta. El resultado de la
			//consulta se devuelve en una variable del tipo puntero a
			//MYSQL_RES tal y como hemos declarado anteriormente.
			//Se trata de una tabla virtual en memoria que es la copia
			//de la tabla real en disco.
			resultado = mysql_store_result (conn);
			// El resultado es una estructura matricial en memoria
			// en la que cada fila contiene los datos de una persona.
			
			// Ahora obtenemos la primera fila que se almacena en una
			// variable de tipo MYSQL_ROW
			row = mysql_fetch_row (resultado);
			// En una fila hay tantas columnas como datos tiene una
			// persona. En nuestro caso hay tres columnas: dni(row[0]),
			// nombre(row[1]) y edad (row[2]).
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "1/No se han obtenido datos la consulta");
			}
			else
			{
				while (row !=NULL) 
				{
					// la columna 2 contiene una palabra que es la edad
					// la convertimos a entero 
					id = atoi (row[0]);
					// las columnas 0 y 1 contienen DNI y nombre 
					printf ("Los datos del jugador son id: %d, user: %s, password: %s\n", id, row[1], row[2]);
					// obtenemos la siguiente fila
					row = mysql_fetch_row (resultado);
				}
			}
			strcpy(respuesta, "1/Usuario registrado");
		}
		
		else if(codigo==2) //quieren iniciar sesion
		{
			p = strtok (NULL, "/");
			strcpy (password, p);
			
			char passwordBBDD[20];
			
			// construimos la consulta SQL
			strcpy (consulta,"SELECT password FROM T3_Juego.jugador WHERE jugador.nombre = '"); 
			strcat (consulta, user);
			strcat (consulta,"'");
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "2/Error al consultar la base de datos");
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "2/El usuario no existe en la base de datos");
			}
			else
			{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				
				strcpy(passwordBBDD, row[0]);
				printf ("El password del jugador: %s, es: %s\n", user, passwordBBDD );
				if(strcmp(passwordBBDD,password)==0)
				{
					strcpy(respuesta, "2/Usuario iniciado");
					res= PonConectado(&miLista, user, sock_conn);
					if(res==-1)
						printf("No se ha podido añadir conectado porque la lista esta llena\n");
					else
						printf("Añadido a la lista de conectados\n");
					char misconectados[300];
					DameConectados(&miLista, misconectados);
					printf("Resultado: %s\n", misconectados);
				}
				else
				   strcpy(respuesta, "2/Datos de inicio de sesión incorrectos");
				
			}
			
			
		}
		
		else if(codigo==3)//Nos da el jugador que ganó la partida en menor tiempo
		{
			strcpy (consulta,"SELECT jugador.nombre FROM T3_Juego.jugador,T3_Juego.relacion,T3_Juego.partida WHERE partida.tiempo_stop = (SELECT MIN(partida.tiempo_stop) FROM T3_Juego.partida) AND partida.id = relacion.idPartida");
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "3/Error al consultar la base de datos");
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "3/No se han obtenido datos en la consulta");
			}
			else
			{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				strcpy(respuesta, "3/El jugador que ganó la partida en menor tiempo es: "); 
				strcat (respuesta, row[0]);
				strcat (respuesta,".");
			}
		}
		
		else if(codigo==4) //Nos da el menor tiempo en ganar del usuario pedido
		{
			
			//Construimos la consulta
			strcpy (consulta,"SELECT partida.tiempo_stop FROM T3_Juego.jugador,T3_Juego.relacion,T3_Juego.partida WHERE jugador.nombre = '"); 
			strcat (consulta, user);
			strcat (consulta,"' AND jugador.id=relacion.idJugador AND relacion.idPartida=partida.id");
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) 
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "4/Error al consultar la base de datos");
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "4/No se han obtenido datos en la consulta");
			}
			else
			{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el nombre
				strcpy(respuesta, "4/El menor tiempo en ganar de user: "); 
				strcat (respuesta, user);
				strcat (respuesta," es: ");
				strcat (respuesta, row[0]);
				strcat (respuesta,".");
			}
		}
		
		else if (codigo==5) //Nos da el numero de partidas ganadas por el jugador
		{
			int cont;
			//Construimos la consulta
			strcpy (consulta,"SELECT partida.ganador FROM T3_Juego.partida ");
			// hacemos la consulta 
			err=mysql_query (conn, consulta); 
			if (err!=0) 
			{
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta, "5/Error al consultar la base de datos");
				exit (1);
			}
			//recogemos el resultado de la consulta 
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "5/No se han obtenido datos en la consulta");
			}
			else
			{
				cont=0;
				while(row!=NULL)
				{
					printf("%s \n", row[0]);
					if(strcmp(row[0], user)==0)
						cont++;
					row=mysql_fetch_row(resultado);
				}
			}
			// El resultado debe ser una matriz con una sola fila
			// y una columna que contiene el nombre
			char cont1;
			strcpy(respuesta, "5/El numero de partidas de user: "); 
			strcat (respuesta, user);
			strcat (respuesta," ganadas es: ");
			sprintf(&cont1, "%d", cont);
			strcat (respuesta, &cont1);
			strcat (respuesta,".");
		}
		else if(codigo==6) //Recibimos la invitación para crear una partida
		{
			int i=0;
			int encontrado=0;
			int sock_invitacion;
			
			p=strtok(NULL,"/");
			strcpy(invitado,p);
			//ya tenemos el nombre del invitado
			printf("Invitado: %s\n", invitado);
			//cogemos el socket del invitador y buscamos su user para añadirlo al mensaje
			while((i<miLista.num)&&(!encontrado))
			{
				if(sock_conn==miLista.conectados[i].socket)
					encontrado=1;
				else
					i++;
			}
			if(encontrado==1)
									sprintf(invitador, miLista.conectados[i].nombre);
			printf("Invitador: %s\n", invitador);
			
			//añadimos la respuesta que queremos que envie al invitado
			strcpy(respuesta, "7/"); 
			strcat (respuesta, invitador);
			printf("Respuesta: %s\n", respuesta);
			
			i=0;
			encontrado=0;
			
			// buscamos el socket del invitado y le enviamos el mensaje "7/Invitador"
			while((i<miLista.num)&&(encontrado==0))
			{
				if(strcmp(miLista.conectados[i].nombre, invitado)==0)
					encontrado=1;
				else
					i++;
			}
			if(encontrado!=0)
			{
				sock_invitacion=miLista.conectados[i].socket;
				write(sock_invitacion, respuesta, strlen(respuesta));
			}
		}
		else if(codigo==7) //Recibimos la contestación a la invitación 
		{
			int i=0;
			int encontrado=0;
			int sock_invitacion;
			char invitacion[20];
			
			p=strtok(NULL,"/");
			strcpy(invitacion,p);
			printf("Invitacion: %s\n", invitacion);
			if(strcmp(invitacion, "Rechazada")==0)
			{
				p=strtok(NULL,"/");
				strcpy(invitador, p);
				printf("invitador: %s\n", invitador);
				//cogemos el socket del invitado
				while((i<miLista.num)&&(!encontrado))
				{
					if(sock_conn==miLista.conectados[i].socket)
						encontrado=1;
					else
						i++;
				}
				if(encontrado==1)
					sprintf(invitado, miLista.conectados[i].nombre);
				
				printf("invitado: %s\n", invitado);
				//ponemos la respuesta que queremos enviar al invitador
				strcpy(respuesta, "8/Rechazada/");
				strcat(respuesta, invitado);
				printf("Respuesta: %s\n", respuesta);
			}
			else
			{
				p=strtok(NULL,"/");
				strcpy(palabra, p);
				p=strtok(NULL,"/");
				strcpy(invitador, p);
				//cogemos el socket del invitado
				while((i<miLista.num)&&(!encontrado))
				{
					if(sock_conn==miLista.conectados[i].socket)
						encontrado=1;
					else
						i++;
				}
				if(encontrado==1)
					sprintf(invitado, miLista.conectados[i].nombre);
				//ponemos la respuesta que queremos
				strcpy(respuesta, "8/Aceptada/");
				strcat(respuesta, palabra);
				strcat(respuesta, "/");
				strcat(respuesta, invitado);
				
				printf("invitador: %s\n", invitador);
				printf("invitado: %s\n", invitado);
				printf("Respuesta: %s\n", respuesta);
			}
			i=0;
			encontrado=0;
			printf("Invitado %s", invitado);
			printf("Invitador %s", invitador);
			//buscamos el socket del invitador
			while((i<miLista.num)&&(encontrado==0))
			{
				if(strcmp(miLista.conectados[i].nombre, invitador)==0)
					encontrado=1;
				else
					i++;
			}
			if(encontrado!=0)
			{
				sock_invitacion= miLista.conectados[i].socket;
				write(sock_invitacion, respuesta, strlen(respuesta));
			}
		}
		
		else if(codigo==8) //Recibimos la palabra del invitado
		{
			
			char invitacion[20];
			int i=0;
			int encontrado=0;
			int sock_invitacion;
			
			p=strtok(NULL,"/");
			strcpy(invitacion,p);
			if(invitacion =="Rechazada")
			{
				p=strtok(NULL, "/");
				strcpy(invitado, p);
				while((i<miLista.num)&&(!encontrado))
				{
					if(sock_conn==miLista.conectados[i].socket)
						encontrado=1;
					else
						i++;
				}
				if(encontrado==1)
										sprintf(invitador, miLista.conectados[i].nombre);
				printf("Invitador: %s\n", invitador);
				strcpy(respuesta, "9/Rechazada/");
				strcat(respuesta, invitador);
				printf("Respuesta: %s\n", respuesta);
				
			}
			else
			{
				p=strtok(NULL,"/");
				strcpy(palabra, p);
				p=strtok(NULL,"/");
				strcpy(invitado,p);
				while((i<miLista.num)&&(!encontrado))
				{
					if(sock_conn==miLista.conectados[i].socket)
						encontrado=1;
					else
						i++;
				}
				if(encontrado==1)
										sprintf(invitador, miLista.conectados[i].nombre);
				printf("Invitador: %s\n", invitador);
				strcpy(respuesta, "9/Aceptada/");
				strcat(respuesta, palabra);
				strcat(respuesta, "/");
				strcat(respuesta, invitador);
				printf("Respuesta: %s\n", respuesta);
			}
			printf("Invitado %s", invitado);
			printf("Invitador %s", invitador);
			i=0;
			encontrado=0;
			// buscamos el socket del invitado y le enviamos el mensaje "7/Invitador"
			while((i<miLista.num)&&(encontrado==0))
			{
				if(strcmp(miLista.conectados[i].nombre, invitado)==0)
					encontrado=1;
				else
					i++;
			}
			if(encontrado!=0)
			{
				sock_invitacion=miLista.conectados[i].socket;
				write(sock_invitacion, respuesta, strlen(respuesta));
			}
		}
		//envia una frase
/*		else if (codigo==9) *///envia una frase
/*		{*/
/*			char frase[100];*/
/*			p = strtok (NULL, "/");*/
/*			strcpy (frase,p);*/
/*			if (Inapropiada (frase)==-1)*/
/*			{*/
/*				strcpy (respuesta1, "10/");*/
/*				write(sock_conn, respuesta1,strlen (respuesta1));*/
/*			}*/
/*			else */
/*			{*/
/*				strcpy(respuesta1,"9/");*/
/*				write(sock_conn,respuesta1,strlen(respuesta1));*/
				//Enviar frases a activos
/*				sprintf(notificacion,"11/%s",frase);*/
/*				int i;*/
/*				for(i=0; i< listaActivos.num; i++)*/
/*					write(listaActivos.sockets[i].socket, notificacion,strlen (notificacion));*/
					
/*			}*/
			
/*		}		*/
		
		
		
		if((codigo!=0)&&(codigo!=6)&&(codigo!=7)&&(codigo!=8))
		{
			printf("Respuesta: %s\n", respuesta);
			//Enviamos respuesta
			write(sock_conn, respuesta, strlen(respuesta));
			pthread_mutex_lock(&mutex);//No me interrumpas
			contadorservicios++;
			pthread_mutex_unlock(&mutex);//Ya puedes interrumpir
			//notificar a todos los clientes conectados
			char notificacion[100];
			DameConectados(&miLista, misconectados);
			sprintf(notificacion, "6/%s", misconectados);
			int j;
			for(j=0; j< misSockets.num; j++)
			{
				write(misSockets.sockets[j].socket, notificacion, strlen(notificacion));
			}
		}
	}
	char user[20];
	DameUser (&miLista, sock_conn, user);
	res= EliminaConectado(&miLista, user);
	if(res==-1)
		printf("No se ha podido quitar conectado de la lista");
	else
		printf("Quitado de la lista de conectados");
	DameConectados(&miLista, misconectados);
	sprintf(notificacion, "6/%s", misconectados);
	int j;
	for(j=0; j< misSockets.num; j++)
	{
		write(misSockets.sockets[j].socket, notificacion, strlen(notificacion));
	}
	
	//Quitamos el socket de la lista 
	//Se acabó el servicio para este cliente
	pthread_mutex_lock( &mutex );
	//EliminaSocket(&listaActivos,sock_conn);
	EliminaSocket(&misSockets, sock_conn);
	close(sock_conn);
	
}

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	//INICIALIZAMOS
	//abrimos socket
	if((sock_listen =socket(AF_INET, SOCK_STREAM, 0)) <0)
		printf("Error creando el socket");
	//Hacemos el bind al puerto
	
	memset(&serv_adr,0,sizeof(serv_adr)); //inicializamos a cero serv_addr
	serv_adr.sin_family = AF_INET;
	
	//asocia el socket a cualquiera de las ip de la maquina
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr=htonl(INADDR_ANY);
	//establecemos el puerto de escucha
	serv_adr.sin_port=htons(9800);
	if(bind(sock_listen,(struct sockaddr *) &serv_adr, sizeof(serv_adr))<0)
		printf("Error en el bind");
	if(listen(sock_listen,3) <0)
		printf("Error en el listen");
	
	//int i;
	int sockets[100];
	// para iniciar la lista de activos 
	//listaActivos.num = 0;
	
	
	pthread_t thread;
	
	
	//i=0;
	//Bucle para atender a clientes
	for(;;){
		printf("Escuchando\n");
		printf(&misSockets, misSockets);
		
		sock_conn =accept(sock_listen, NULL, NULL);
		printf("He recibido conexion\n");
		pthread_mutex_lock( &mutex);
		pthread_mutex_unlock(  &mutex);
	
		
		int res=PonSocket(&misSockets, sock_conn);
		if(res==-1)
			printf("No se ha podido a�adir a la lista de sockets\n");
		else
		{
			// Crear thead y decirle lo que tiene que hacer
			pthread_create (&thread, NULL, AtenderCliente, &sock_conn);
			printf("Socket a�adido correctamente\n");
			char missockets[100];
			DameSockets(&misSockets, missockets);
			printf("Resultado: %s\n", missockets);
			
		}
		//sockets[i] = sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		
		//Crear thread y decirle lo que tiene que hacer
		//pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);
		//i=i+1;
		
	}
	
	//Bucle para atender a 5 clientes
	//for (i=0; i<5; i++)
	//	pthread_join (thread[i], NULL);
}

