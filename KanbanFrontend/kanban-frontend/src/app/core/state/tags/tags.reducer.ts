import { TagDto } from '../../models/DTOs/tag.model';

export interface TagState {
    tags: TagDto[];
    error: string | null;
    status: 'idle' | 'loading' | 'success' | 'error' | 'creating' | 'updating' | 'deleting';
}

export const initialTagsState: TagState = {
    tags: [],
    error: null,
    status: 'idle',
};
