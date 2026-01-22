import { LogState } from './activity-log/activity-log.reducer';
import { BoardState } from './boards/boards.reducer';
import { ColumnState } from './columns/columns.reducer';
import { CommentState } from './comments/comments.reducer';
import { TagState } from './tags/tags.reducer';
import { TaskState } from './tasks/tasks.reducer';
import { UserState } from './users/users.reducer';

export interface AppState {
    tasksState: TaskState;
    logsState: LogState;
    boardsState: BoardState;
    columnsState: ColumnState;
    commentsState: CommentState;
    tagsState: TagState;
    usersState: UserState;
}
