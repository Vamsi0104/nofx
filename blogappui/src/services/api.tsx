const API_BASE = 'https://localhost:7144/api';

export const getPosts = async () => {
    const res = await fetch(`${API_BASE}/posts`);
    return res.json();
};

export const createPost = async (post: { title: string; content: string }) => {
    const res = await fetch(`${API_BASE}/posts`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(post),
    });
    return res.json();
};