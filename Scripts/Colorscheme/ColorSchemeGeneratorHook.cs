namespace Chinchillada.Colors
{
    using System.Collections.Generic;
    using System.Linq;
    using Generation;
    using UnityEngine;
    using Utilities;

    public class ColorSchemeGeneratorHook : ChinchilladaBehaviour
    {
        [SerializeField, FindComponent] private IGenerator<IColorScheme> generator;

        [SerializeField] private List<IColorschemeUser> users = new List<IColorschemeUser>();

        public void Register(IColorschemeUser user) => users.Add(user);

        public void Deregister(IColorschemeUser user) => users.Remove(user);

        private void CleanUsers()
        {
            for (var i = users.Count - 1; i >= 0; i--)
            {
                if (users[i] != null)
                    continue;

                var lastIndex = users.Count - 1;

                users[i] = users[lastIndex];
                users.RemoveAt(lastIndex);
            }
        }

        private void FindUsers()
        {
            var newUsers = GetComponentsInChildren<IColorschemeUser>();
            foreach (var user in newUsers)
            {
                if (!users.Contains(user)) 
                    users.Add(user);
            }
        }

        private void OnEnable()
        {
            CleanUsers();
            FindUsers();

            if (generator != null)
                generator.Generated += UpdateColorScheme;
        }

        private void OnDisable() => generator.Generated -= UpdateColorScheme;

        private void UpdateColorScheme(IColorScheme colorScheme)
        {
            foreach (var user in users.Where(user => user != null))
                user.ColorScheme = colorScheme;
        }
    }
}