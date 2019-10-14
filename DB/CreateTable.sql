
CREATE USER 'indiamazones'@'%' IDENTIFIED BY PASSWORD '***';

CREATE DATABASE 'indiamazones';

CREATE TABLE `tb_object` (
  `object_id` int(11) NOT NULL,
  `object_name` varchar(100) NOT NULL,
  `object_score` int(11) DEFAULT NULL,
  `difficulty` int(11) DEFAULT NULL,
  `log_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `object_name_ko` varchar(100) DEFAULT NULL,
  `object_name_cn` varchar(100) DEFAULT NULL,
  `object_name_ja` varchar(100) DEFAULT NULL,
  `object_name_es` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`object_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_game` (
  `game_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `start_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `end_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`game_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_game_result` (
  `game_id` int(11) NOT NULL,
  `total_score` int(11) DEFAULT NULL,
  `total_rank` bigint(20) DEFAULT NULL,
  `total_found_objects` int(11) DEFAULT NULL,
  `total_playtime` int(11) DEFAULT NULL,
  `name` varchar(2083) DEFAULT NULL,
  PRIMARY KEY (`game_id`),
  CONSTRAINT `tb_game_result_ibfk_1` FOREIGN KEY (`game_id`) REFERENCES `tb_game` (`game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_stage_log` (
  `game_id` int(11) NOT NULL,
  `stage_id` int(11) NOT NULL,
  `objects_score` int(11) DEFAULT NULL,
  `time_score` int(11) DEFAULT NULL,
  `clear_score` int(11) DEFAULT NULL,
  `stage_score` int(11) DEFAULT NULL,
  `completed_yn` char(1) NOT NULL,
  `start_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `end_date` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `total_score` int(11) DEFAULT NULL,
  PRIMARY KEY (`game_id`,`stage_id`),
  CONSTRAINT `tb_stage_log_ibfk_1` FOREIGN KEY (`game_id`) REFERENCES `tb_game` (`game_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_stage_object` (
  `game_id` int(11) NOT NULL,
  `stage_id` int(11) NOT NULL,
  `object_name` varchar(100) NOT NULL,
  `object_score` int(11) DEFAULT NULL,
  `found_yn` char(1) NOT NULL,
  `file_loc` varchar(2083) DEFAULT NULL,
  `log_date` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`game_id`,`stage_id`,`object_name`),
  CONSTRAINT `tb_stage_object_ibfk_1` FOREIGN KEY (`game_id`) REFERENCES `tb_game` (`game_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
