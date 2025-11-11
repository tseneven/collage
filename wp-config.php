<?php
/**
 * Основные параметры WordPress.
 *
 * Скрипт для создания wp-config.php использует этот файл в процессе установки.
 * Необязательно использовать веб-интерфейс, можно скопировать файл в "wp-config.php"
 * и заполнить значения вручную.
 *
 * Этот файл содержит следующие параметры:
 *
 * * Настройки базы данных
 * * Секретные ключи
 * * Префикс таблиц базы данных
 * * ABSPATH
 *
 * @link https://ru.wordpress.org/support/article/editing-wp-config-php/
 *
 * @package WordPress
 */

// ** Параметры базы данных: Эту информацию можно получить у вашего хостинг-провайдера ** //
/** Имя базы данных для WordPress */
define( 'DB_NAME', 'neven_bd' );

/** Имя пользователя базы данных */
define( 'DB_USER', 'root' );

/** Пароль к базе данных */
define( 'DB_PASSWORD', '' );

/** Имя сервера базы данных */
define( 'DB_HOST', 'localhost' );

/** Кодировка базы данных для создания таблиц. */
define( 'DB_CHARSET', 'utf8mb4' );

/** Схема сопоставления. Не меняйте, если не уверены. */
define( 'DB_COLLATE', '' );

/**#@+
 * Уникальные ключи и соли для аутентификации.
 *
 * Смените значение каждой константы на уникальную фразу. Можно сгенерировать их с помощью
 * {@link https://api.wordpress.org/secret-key/1.1/salt/ сервиса ключей на WordPress.org}.
 *
 * Можно изменить их, чтобы сделать существующие файлы cookies недействительными.
 * Пользователям потребуется авторизоваться снова.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         '_Uf.yx3+^kymmW[khIe.p<l}XMkO>Dj?~J(0|5{hM=l95>=|5t=mKgn7mWHdch5(' );
define( 'SECURE_AUTH_KEY',  'xaj:wAd1XyR/Me;JG`(pL]}NIZ1ewRoJ/ SEKPU_>p&9efxrnQd7%kA}=0GQ*_@ ' );
define( 'LOGGED_IN_KEY',    '4^t~ozs5HYjF2r?.ju}i,F8[;Y{,!lyb7}Z4=v}A%>>NGu*{E2ohd>tnW 1$:if ' );
define( 'NONCE_KEY',        'a@WHldMmDz]m0Z0CeWre~p=9r$ kC>18V T(32ENaq*`hYHJqUCgn|&8dGul!3qE' );
define( 'AUTH_SALT',        'VB/Fj$m)q=j(YC2n<(}iG^f3gGipXXae%(8%F$6)?18 pcV`%BM5UxsrEh:Mh[j ' );
define( 'SECURE_AUTH_SALT', '[=75kk^T!&K!2,X0<F<2`.$D6 HhEa+4+]Lu7ZC^?nx5(u-.KgHdX8dAuFCKm!aG' );
define( 'LOGGED_IN_SALT',   '2xVi|12p1z$-dy*=sPikhz-X`l>H3K?B]HWe332Jz:qw7qi8&D!sLmpOcVz(jE=P' );
define( 'NONCE_SALT',       'Ma#&h3~!7fdu g8T?fpLHY}gh9H]*KIx/VH{AjDnmc~7C)`X#GGi+93;cX&|*0FP' );

/**#@-*/

/**
 * Префикс таблиц в базе данных WordPress.
 *
 * Можно установить несколько сайтов в одну базу данных, если использовать
 * разные префиксы. Пожалуйста, указывайте только цифры, буквы и знак подчеркивания.
 */
$table_prefix = 'wp_';

/**
 * Для разработчиков: Режим отладки WordPress.
 *
 * Измените это значение на true, чтобы включить отображение уведомлений при разработке.
 * Разработчикам плагинов и тем настоятельно рекомендуется использовать WP_DEBUG
 * в своём рабочем окружении.
 *
 * Информацию о других отладочных константах можно найти в документации.
 *
 * @link https://ru.wordpress.org/support/article/debugging-in-wordpress/
 */
define( 'WP_DEBUG', false );

/* Произвольные значения добавляйте между этой строкой и надписью "дальше не редактируем". */



/* Это всё, дальше не редактируем. Успехов! */

/** Абсолютный путь к директории WordPress. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Инициализирует переменные WordPress и подключает файлы. */
require_once ABSPATH . 'wp-settings.php';
